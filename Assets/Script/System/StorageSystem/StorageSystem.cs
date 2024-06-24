using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface IStorageSystem : ISystem
    {
        void AddItemToStorageUnit(StorageUpdateInfo info);

        void RemoveItemFromStorageUnit(StorageUpdateInfo info);

        int FindSameItemAndGetSum(ISlot[] slots, IItem item);
    }

    public class StorageSystem : AbstractSystem, IStorageSystem
    {
        List<ISlot> canStackSlots = new List<ISlot>();
        List<ISlot> sameItemSlots = new List<ISlot>();

        protected override void OnInit()
        {
            var backPack = this.GetModel<IPlayerModel>().BackPack;
            UpdateUnitNullSlotsList(backPack); //todo...
        }

        public void AddItemToStorageUnit(StorageUpdateInfo info)
        {
            FindCanStackSlots(info.TargetUnit.Slots, info.Item);
            if (info.TargetUnit.NullSlots.Count == 0 && canStackSlots.Count == 0)
            {
                Debug.Log("slot is not enough, lost nums:" + info.Nums);
                return;
            }

            AddItemToCanStackOrNullSlots(info.TargetUnit.NullSlots, info.Item, info.Nums);
            canStackSlots.Clear();
        }

        public void RemoveItemFromStorageUnit(StorageUpdateInfo info)
        {
            int sum = FindSameItemAndGetSum(info.TargetUnit.Slots, info.Item);
            if (sum < info.Nums)
            {
                Debug.Log("Item is not enough!! still need Nums: " + (info.Nums - sum));
                return;
            }

            HandleMinusItemFromUnit(info);
        }

        public int FindSameItemAndGetSum(ISlot[] slots, IItem item)
        {
            sameItemSlots.Clear();
            int sum = 0;
            foreach (var slot in slots)
            {
                if (slot.Item == null)
                    continue;
                if (slot.Item.ID == item.ID)
                {
                    sameItemSlots.Add(slot);
                    sum += slot.Nums;
                }
            }

            return sum;
        }

        #region ì‡ïîóp

        void AddItemToCanStackOrNullSlots(List<ISlot> nullSlot, IItem item, int Nums)
        {
            {
                while (Nums > 0 && canStackSlots.Count > 0)
                {
                    Nums = StackIntoCanStackSlots(Nums);
                }

                while (Nums > 0 && nullSlot.Count > 0)
                {
                    Nums = StackIntoNullSlots(nullSlot, item, Nums);
                }

                if (Nums > 0)
                {
                    Debug.Log("slot is not enough, lost nums:" + Nums); //ÇµÇŒÇÁÇ≠Ç±ÇÃèàóù todo...
                }
            }
        }

        void FindCanStackSlots(ISlot[] slots, IItem item)
        {
            foreach (var slot in slots)
            {
                if (slot.Item == null)
                    continue;
                //ÇµÇŒÇÁÇ≠99 todo...
                if (slot.Item.ID == item.ID && slot.Nums < 99) //maxStackNums should be customs todo...
                {
                    canStackSlots.Add(slot);
                }
            }
        }

        void UpdateUnitNullSlotsList(IStorageUnit unit)
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
            int canStackNum = 99 - canStackSlots[0].Nums; //maxStackNums should be customs todo...
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

        int StackIntoNullSlots(List<ISlot> nullSlots, IItem item, int Nums)
        {
            nullSlots[0].Item = item;
            if (Nums < 99)
            {
                nullSlots[0].Nums = Nums;
                Nums -= Nums;
            }
            else
            {
                nullSlots[0].Nums = 99; //maxStackNums should be customs todo...
                Nums -= nullSlots[0].Nums;
            }

            nullSlots.Remove(nullSlots[0]);
            return Nums;
        }

        void HandleMinusItemFromUnit(StorageUpdateInfo info)
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

            UpdateUnitNullSlotsList(info.TargetUnit);
        }

        #endregionÅ@ì‡ïîóp
    }
}