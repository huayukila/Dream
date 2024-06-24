using System.Collections.Generic;
using UnityEngine;

public struct StorageUpdateInfo
{
    public IItem Item;
    public int Nums;
    public IStorageUnit TargetUnit;
}

public interface IStorageUnit
{
    int UnitSize { get; }
    ISlot[] Slots { get; }
    List<ISlot> NullSlots { get; }
}

public class StorageUnit : IStorageUnit
{
    public int UnitSize { get; }
    public ISlot[] Slots { get; }
    public List<ISlot> NullSlots { get; set; }

    /// <summary>
    /// maxSize.x is Rows, maxSize.y is Columns
    /// </summary>
    /// <param name="maxSize"></param>
    public StorageUnit(int maxSize)
    {
        NullSlots = new List<ISlot>();
        UnitSize = maxSize;
        Slots = new Slot[maxSize];
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i] = new Slot();
        }
    }
}