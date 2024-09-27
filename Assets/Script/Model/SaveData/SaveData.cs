using System;
using System.Collections.Generic;

namespace Framework.Farm
{
    [Serializable]
    public class SaveData
    {
        public int ID;
        public string RealTime;
        public string GameTime;
        public PlayerData PlayerData;
        public List<StorageUnitData> StorageUnitDatas;
    }

    [Serializable]
    public struct PlayerData
    {
        public string BackPackID;
        public int FoodValue;
        public int WaterValue;
    }

    [Serializable]
    public struct StorageUnitData
    {
        public string id;
        public int size;
        public List<SlotData> slots;
    }

    [Serializable]
    public class SlotData
    {
        public int amount=0;
        public int id=-1;
    }

    [Serializable]
    public class GameSettingData
    {
        //Graphics
        public bool isWindows = true;

        //sound
        public float mainSoundVolume = 1.0f;
        //Log

        //HotKey
    }
}