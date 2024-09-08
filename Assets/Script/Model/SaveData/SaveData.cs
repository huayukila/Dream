using System;
using System.Collections.Generic;

namespace Framework.Farm
{
    [Serializable]
    public class SaveData
    {
        public int ID;
        public PlayerData PlayerData;
        public List<StorageUnitData> StorageUnitDatas;
    }

    [Serializable]
    public struct PlayerData
    {
        public int FoodValue;
        public int WaterValue;
    }

    [Serializable]
    public struct StorageUnitData
    {
        public int index;
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