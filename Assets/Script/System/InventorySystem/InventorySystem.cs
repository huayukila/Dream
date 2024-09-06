using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Framework.Farm
{
    public interface IInventorySystem : ISystem
    {
        InventoryUpdateResult AddItemToStorageUnit(InventoryUpdateInfo info);

        void RemoveItemFromStorageUnit(InventoryUpdateInfo info);

        List<ISlot> FindSameItemAndGetSlotsByItemID(IInventoryUnit unit, int itemID);

        int GetSameItemsSumFromUnitByItemID(IInventoryUnit unit, int itemID);

        int GetSameItemSumFromSlotsByItemID(List<ISlot> slots, int ItemID);

        void DragItemToSlot(SlotUI handlingSlotUI, SlotUI targetSlotUI);
    }

    public class InventorySystem : AbstractSystem, IInventorySystem
    {
        private IItemConfigModel _itemConfigModel;
        private Dictionary<int, InventoryUnit> _unitsGroup = new Dictionary<int, InventoryUnit>();
        List<ISlot> canStackSlots = new List<ISlot>();

        protected override void OnInit()
        {
            {
                //セーフデータから初期化
                //todo...
                var backPack = this.GetModel<IPlayerModel>().BackPack;
                _itemConfigModel = this.GetModel<IItemConfigModel>();

                //模擬
                var itemConfigObj = _itemConfigModel.GetConfigByID(1001);
                CreateItemByIndex(backPack, itemConfigObj, 1, 0);

                UpdateUnitNullSlotsList(backPack);
                _unitsGroup.Add(backPack.Index, backPack);
            }
        }

        public InventoryUpdateResult AddItemToStorageUnit(InventoryUpdateInfo info)
        {
            var config = this.GetModel<IItemConfigModel>().GetConfigByID(info.ItemId);
            FindCanStackSlots(info.TargetUnit, config);
            if (info.TargetUnit.NullSlots.Count == 0 && canStackSlots.Count == 0)
            {
                Debug.Log("slot is not enough, lost nums:" + info.Nums);
                return new InventoryUpdateResult() { ItemID = info.ItemId, Nums = info.Nums };
            }

            var result = AddItemToCanStackOrNullSlots(info.TargetUnit.NullSlots, config, info.Nums);
            canStackSlots.Clear();
            return result;
        }


        public void RemoveItemFromStorageUnit(InventoryUpdateInfo info)
        {
            var tempList = FindSameItemAndGetSlotsByItemID(info.TargetUnit, info.ItemId);
            int sum = GetSameItemSumFromSlotsByItemID(tempList, info.ItemId);
            if (sum < info.Nums)
            {
                Debug.Log("Item is not enough!! still need Nums: " + (info.Nums - sum));
                return;
            }

            HandleRemoveItemFromSlotsByInfo(tempList, info);
            tempList.Clear();
            UpdateUnitNullSlotsList(info.TargetUnit);
        }


        public List<ISlot> FindSameItemAndGetSlotsByItemID(IInventoryUnit unit, int itemID)
        {
            List<ISlot> sameItemSlots = new List<ISlot>();
            foreach (var slot in unit.Slots)
            {
                if (slot.Item == null)
                    continue;
                if (slot.Item.ID == itemID)
                {
                    sameItemSlots.Add(slot);
                }
            }

            return sameItemSlots;
        }

        public int GetSameItemsSumFromUnitByItemID(IInventoryUnit unit, int itemID)
        {
            var tempList = FindSameItemAndGetSlotsByItemID(unit, itemID);
            int sum = GetSameItemSumFromSlotsByItemID(tempList, itemID);
            return sum;
        }

        public int GetSameItemSumFromSlotsByItemID(List<ISlot> slots, int ItemID)
        {
            int sum = 0;
            foreach (var slot in slots)
            {
                sum += slot.Nums;
            }

            return sum;
        }

        public void DragItemToSlot(SlotUI handlingSlotUI, SlotUI targetSlotUI)
        {
            var cachedItem = targetSlotUI.Data.Item;
            var cachedItemNums = targetSlotUI.Data.Nums;

            var handlingItem = handlingSlotUI.Data.Item;
            var handlingItemNums = handlingSlotUI.Data.Nums;


            if (cachedItem != null)
            {
                if (cachedItem.ID == handlingItem.ID)
                {
                    if (cachedItem.IsCanStack)
                    {
                        if (cachedItemNums == cachedItem.MaxStackNums)
                            return;
                        int sum = cachedItemNums + handlingItemNums;

                        if (cachedItem.MaxStackNums >= sum)
                        {
                            targetSlotUI.Data.Nums = sum;
                            handlingSlotUI.Data.Nums = 0;
                        }
                        else
                        {
                            handlingSlotUI.Data.Nums -= targetSlotUI.Data.Item.MaxStackNums - cachedItemNums;
                            targetSlotUI.Data.Nums = cachedItem.MaxStackNums;
                        }
                    }
                }
            }


            if (cachedItem == null || cachedItem.ID != handlingItem.ID)
            {
                var tempItem = cachedItem;
                targetSlotUI.Data.Item = handlingSlotUI.Data.Item;
                targetSlotUI.Data.Nums = handlingSlotUI.Data.Nums;

                handlingSlotUI.Data.Nums = cachedItemNums;
                handlingSlotUI.Data.Item = tempItem;
            }
            // if (handlingSlotUI.Data.ParentInventoryUnit != targetSlotUI.Data.ParentInventoryUnit)
            // {
            //     var cachedUnit = targetSlotUI.Data.ParentInventoryUnit;
            //     targetSlotUI.Data.ChangeParentInventoryUnit(handlingSlotUI.Data.ParentInventoryUnit);
            //     handlingSlotUI.Data.ChangeParentInventoryUnit(cachedUnit);
            //     UpdateUnitNullSlotsList(targetSlotUI.Data.ParentInventoryUnit);
            // }

            UpdateUnitNullSlotsList(handlingSlotUI.Data.ParentInventoryUnit);
        }

        #region 内部用

        InventoryUpdateResult AddItemToCanStackOrNullSlots(List<ISlot> nullSlot, ItemConfigObj config, int Nums)
        {
            {
                while (Nums > 0 && canStackSlots.Count > 0)
                {
                    Nums = StackIntoCanStackSlots(Nums);
                }

                while (Nums > 0 && nullSlot.Count > 0)
                {
                    Nums = StackIntoNullSlots(nullSlot, config, Nums);
                }

                if (Nums > 0)
                {
                    Debug.Log("slot is not enough, lost nums:" + Nums); //しばらくこの処理 todo...
                }
            }
            var temp = new InventoryUpdateResult() { ItemID = config.UID, Nums = Nums };
            return temp;
        }

        void FindCanStackSlots(IInventoryUnit unit, ItemConfigObj config)
        {
            if (!config.IsCanStack)
            {
                return;
            }

            foreach (var slot in unit.Slots)
            {
                if (slot.Item == null)
                    continue;
                if (slot.Item.ID == config.UID && slot.Nums < config.MaxStackNums)
                {
                    canStackSlots.Add(slot);
                }
            }
        }

        void UpdateUnitNullSlotsList(IInventoryUnit unit)
        {
            List<ISlot> nullSlots = unit.NullSlots;
            unit.NullSlots.Clear();
            foreach (var slot in unit.Slots)
            {
                if (slot.Item == null)
                {
                    nullSlots.Add(slot);
                }
            }
        }

        int StackIntoCanStackSlots(int Nums)
        {
            int canStackNum =
                canStackSlots[0].Item.MaxStackNums - canStackSlots[0].Nums;
            if (canStackNum > Nums)
            {
                canStackSlots[0].Nums += Nums;
                return 0;
            }

            canStackSlots[0].Nums += canStackNum;
            Nums -= canStackNum;
            canStackSlots.Remove(canStackSlots[0]);
            return Nums;
        }

        int StackIntoNullSlots(List<ISlot> nullSlots, ItemConfigObj config, int Nums)
        {
            nullSlots[0].Item = new Item(config);

            if (!config.IsCanStack)
            {
                nullSlots[0].Nums = 1;
                Nums -= 1;
            }
            else if (Nums < config.MaxStackNums)
            {
                nullSlots[0].Nums = Nums;
                Nums -= Nums;
            }
            else
            {
                nullSlots[0].Nums = config.MaxStackNums;
                Nums -= nullSlots[0].Nums;
            }

            nullSlots.Remove(nullSlots[0]);
            return Nums;
        }

        void HandleRemoveItemFromSlotsByInfo(List<ISlot> sameItemSlots, InventoryUpdateInfo info)
        {
            int Nums = info.Nums;
            while (Nums > 0)
            {
                if (Nums < sameItemSlots[0].Nums)
                {
                    sameItemSlots[0].Nums -= Nums;
                    Nums = 0;
                }

                if (Nums == sameItemSlots[0].Nums)
                {
                    sameItemSlots[0].Nums = 0;
                    Nums = 0;
                }

                if (Nums > sameItemSlots[0].Nums)
                {
                    Nums -= sameItemSlots[0].Nums;
                    sameItemSlots[0].Nums = 0;
                    sameItemSlots.Remove(sameItemSlots[0]);
                }
            }
        }

        void CreateItemByIndex(InventoryUnit unit, ItemConfigObj obj, int amount = 1, int index = 0)
        {
            unit.Slots[index].Item = new Item(obj);
            unit.Slots[index].Nums = amount;
        }

        #endregion　内部用
    }
}