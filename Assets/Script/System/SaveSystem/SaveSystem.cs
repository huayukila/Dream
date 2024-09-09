using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework.Farm
{
    interface ISaveSystem : ISystem
    {
        void Save(int saveDataID = 0);

        void Load(int saveDataID = 0);

        void CreateNewSaveData(int index = 0);

        void DeleteSaveData(int index);
    }

    public class SaveSystem : AbstractSystem, ISaveSystem
    {
        private ISaveDataModel _saveDataModel;


        private static readonly string saveData = "SaveData";
        private static readonly string gameSetting = "GameSetting";
#if !UNITY_EDITOR
        static string _rootPath = Path.Combine(Application.persistentDataPath, saveData);
#elif UNITY_EDITOR
        private static readonly string _rootPath = Path.Combine(Application.dataPath, saveData);
#endif
        private static readonly string _gameSettingPath = Path.Combine(_rootPath, gameSetting);
        private static readonly string _saveDataPath = Path.Combine(_rootPath, saveData);

        private GameSettingData _gameSettingData; //todo... move to gameSettingModel


        protected override void OnInit()
        {
            _saveDataModel = this.GetModel<ISaveDataModel>();

            if (!Directory.Exists(_rootPath))
            {
                Directory.CreateDirectory(_rootPath);
            }

            if (!File.Exists(_gameSettingPath + ".json"))
            {
                _gameSettingData = new GameSettingData();
                string json = JsonUtility.ToJson(_gameSettingData, true);
                File.WriteAllText(_gameSettingPath + ".json", json);
            }
            else
            {
                string gameSettingJson = File.ReadAllText(_gameSettingPath + ".json");
                _gameSettingData = JsonUtility.FromJson<GameSettingData>(gameSettingJson);
            }
        }

        public void Save(int saveDataID = 0)
        {
            var playerModel = this.GetModel<IPlayerModel>();
            var playerData = new PlayerData()
            {
                BackPackID = playerModel.BackPack.ID.ToString(),
                FoodValue = playerModel.FoodValue,
                WaterValue = playerModel.WaterValue
            };

            var inventorySystem = this.GetModel<IInventoryModel>();

            List<StorageUnitData> unitDatas = new List<StorageUnitData>();

            foreach (var unit in inventorySystem._unitsGroup.Values)
            {
                var slotData = new List<SlotData>();
                foreach (var slot in unit.Slots)
                {
                    int id = 0;
                    if (slot.Item == null)
                    {
                        id = -1;
                    }
                    else
                    {
                        id = slot.Item.ID;
                    }

                    var temp = new SlotData()
                    {
                        amount = slot.Nums,
                        id = id,
                    };
                    slotData.Add(temp);
                }

                unitDatas.Add(new StorageUnitData()
                {
                    id = unit.ID.ToString(),
                    size = unit.UnitSize,
                    slots = slotData
                });
            }

            var saveData = new SaveData()
            {
                ID = saveDataID,
                PlayerData = playerData,
                StorageUnitDatas = unitDatas
            };

            var saveDataJson = JsonUtility.ToJson(saveData);

            var path = _saveDataPath + saveDataID + ".json";
            File.WriteAllText(path, saveDataJson);
            _saveDataModel.SaveDataInfos.TryAdd(saveDataID, new SaveDataInfo()
            {
                Path = path
            });
        }


        public void Load(int saveDataID = 0)
        {
            string path = _saveDataPath + saveDataID + ".json";

            var saveDataJson = File.ReadAllText(path);

            var saveData = JsonUtility.FromJson<SaveData>(saveDataJson);

            Dictionary<Guid, StorageUnitData> tempDic = new Dictionary<Guid, StorageUnitData>();
            foreach (var data in saveData.StorageUnitDatas)
            {
                tempDic.Add(Guid.Parse(data.id), data);
            }

            this.GetSystem<IInventorySystem>().LoadUnitFromSaveData(tempDic);
            HandleLoadPlayerData(saveData.PlayerData);
        }

        public void CreateNewSaveData(int index = 0)
        {
            string playerBackPackID = Guid.NewGuid().ToString();
            var playerData = new PlayerData()
            {
                BackPackID = playerBackPackID,
                FoodValue = GLOBAL.MAX_FOOD_VALUE,
                WaterValue = GLOBAL.MAX_WATER_VALUE
            };
            var storageUnitsData = new StorageUnitData()
            {
                id = playerBackPackID,
                size = GLOBAL.DEFAULT_BACKPACK_SIZE,
                slots = new List<SlotData>(GLOBAL.DEFAULT_BACKPACK_SIZE) { },
            };

            for (int i = 0; i < storageUnitsData.size; i++)
            {
                storageUnitsData.slots.Add(new SlotData());
            }

            var saveData = new SaveData()
            {
                ID = index,
                PlayerData = playerData,
                StorageUnitDatas = new List<StorageUnitData>() { storageUnitsData }
            };

            var saveDataJson = JsonUtility.ToJson(saveData);

            var path = _saveDataPath + index + ".json";
            File.WriteAllText(path, saveDataJson);

            _saveDataModel.SaveDataInfos.Add(index, new SaveDataInfo()
            {
                Path = path
            });
        }

        public void DeleteSaveData(int index)
        {
        }


        #region “à•”—p

        void HandleLoadPlayerData(PlayerData data)
        {
            var playerModel = this.GetModel<IPlayerModel>();
            var id = Guid.Parse(data.BackPackID);
            playerModel.SetBackPack(this.GetModel<IInventoryModel>()._unitsGroup[id]);
            playerModel.FoodValue = data.FoodValue;
            playerModel.WaterValue = data.WaterValue;
        }

        #endregion
    }
}