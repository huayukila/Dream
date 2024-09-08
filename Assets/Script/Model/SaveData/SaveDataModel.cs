using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Framework.Farm
{
    public struct SaveDataInfo
    {
        //sprite

        public string Path;
    }

    public interface ISaveDataModel : IModel
    {
        public Dictionary<int, SaveDataInfo> SaveDataInfos { get; set; }
    }

    public class SaveDataModel : AbstractModel, ISaveDataModel
    {
        public Dictionary<int, SaveDataInfo> SaveDataInfos { get; set; }

        protected override void OnInit()
        {
            SaveDataInfos = new Dictionary<int, SaveDataInfo>();
        }
    }
}