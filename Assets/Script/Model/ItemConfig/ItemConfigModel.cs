using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Farm
{
    public interface IItemConfigModel : IModel
    {
        ItemConfigObj GetConfigByID(int id);
    }

    public class ItemConfigModel : AbstractModel, IItemConfigModel
    {
        private Dictionary<int, ItemConfigObj> ItemConfigDic;

        protected override void OnInit()
        {
            var database = Resources.Load<ItemConfigDatabase>("ItemConfigDatabase");
            ItemConfigDic = new Dictionary<int, ItemConfigObj>();

            foreach (var obj in database.ItemConfigObjs)
            {
                ItemConfigDic.Add(obj.UID, obj);
            }

            Resources.UnloadAsset(database);
        }

        public ItemConfigObj GetConfigByID(int id)
        {
            if (ItemConfigDic.TryGetValue(id, out ItemConfigObj obj))
            {
                return obj;
            }

            return default;
        }
    }
}