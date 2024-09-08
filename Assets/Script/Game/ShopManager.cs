using System.Collections.Generic;
using UnityEngine;

namespace Framework.Farm
{
    public class ShopManager : ProjectCtrler
    {
        public Transform ItemContent;
        public GameObject ShopMenu;
        public BuyItemWindowCtrl BuyItemWindowCtrl;
        public GameObject ShopSlotUIPrefab;
        private InventoryUnit shopUnit;

        private List<ShopSlotUI> slotUIList = new List<ShopSlotUI>();

        // Start is called before the first frame update
        void Start()
        {
            shopUnit = InventoryUnit.CreateStorageUnit(5);

            var itemConfig = this.GetModel<IItemConfigModel>().GetConfigByID(1002);
            var itemConfig2 = this.GetModel<IItemConfigModel>().GetConfigByID(1001);
            shopUnit.Slots[0].Item = new Item(itemConfig);
            shopUnit.Slots[0].Nums = 100;
            shopUnit.Slots[1].Item = new Item(itemConfig2);
            shopUnit.Slots[1].Nums = 3;
            shopUnit.Slots[2].Item = new Item(itemConfig);
            shopUnit.Slots[2].Nums = 40;
            shopUnit.Slots[3].Item = new Item(itemConfig2);
            shopUnit.Slots[3].Nums = 60;

            for (int i = 0; i < shopUnit.Slots.Count; i++)
            {
                if (shopUnit.Slots[i].Item == null)
                    break;
                GameObject slotUIObj = Instantiate(ShopSlotUIPrefab, ItemContent);

                RectTransform rectTrans = slotUIObj.transform as RectTransform;

                if (rectTrans == null)
                    return;

                rectTrans.position += rectTrans.rect.height * Vector3.down * i;
                var slotUI = slotUIObj.GetComponent<ShopSlotUI>();
                slotUIList.Add(slotUI);
                slotUI.InitWithData(shopUnit.Slots[i]);
            }


            // ShopMenu.SetActive(false);
            this.RegisterEvent<BoughtItemEvent>(e => { SlotUIsUpdate(); })
                .UnregisterWhenGameObjectDestroyed(gameObject);
        }

        void SlotUIsUpdate()
        {
            foreach (var slotUI in slotUIList)
            {
                slotUI.UpdateSlotUI();
            }
        }
    }
}