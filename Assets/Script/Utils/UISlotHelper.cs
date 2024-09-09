using System.Collections.Generic;
using UnityEngine;

namespace Framework.Farm.Utils
{
    public class UISlotHelper : MonoBehaviour
    {
        private static UISlotHelper instance;

        public static UISlotHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UISlotHelper();
                    instance.Init();
                }

                return instance;
            }
        }

        private GameObject uiSlotPre;

        private void Init()
        {
            uiSlotPre = Resources.Load<GameObject>("SlotUI");
        }

        //todo... need Pool
        public List<SlotUI> AllocateUISlots(int amount, Transform parent)
        {
            List<SlotUI> tempList = new List<SlotUI>();
            for (int i = 0; i < amount; i++)
            {
                var obj = Instantiate(uiSlotPre, parent, false);
                tempList.Add(obj.GetComponent<SlotUI>());
            }

            return tempList;
        }


        public void RecycleUISlots(List<SlotUI> slots)
        {
            
        }
    }
}