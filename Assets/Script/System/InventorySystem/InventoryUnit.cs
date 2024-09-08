using System.Collections.Generic;
using UnityEngine;

public struct InventoryUpdateInfo
{
    public int ItemId;
    public int Nums;
    public IInventoryUnit TargetUnit;
}

public struct InventoryUpdateResult
{
    public int ItemID;
    public int Nums;
}

public interface IInventoryUnit
{
    int Index { get; }
    int UnitSize { get; }
    List<ISlot> Slots { get; }
    List<ISlot> NullSlots { get; }
}

public class InventoryUnit : IInventoryUnit
{
    private static int INDEX = 1;
    public int Index { get; private set; }
    public int UnitSize { get; }
    public List<ISlot> Slots { get; }
    public List<ISlot> NullSlots { get; set; }

    public static InventoryUnit CreateStorageUnit(int maxSize, out int index)
    {
        index = INDEX;
        var unit = new InventoryUnit(maxSize)
        {
            Index = index
        };
        INDEX++;
        return unit;
    }

    public static InventoryUnit CreateStorageUnit(int maxSize)
    {
        var unit = new InventoryUnit(maxSize)
        {
            Index = INDEX
        };
        INDEX++;
        return unit;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="maxSize"></param>
    private InventoryUnit(int maxSize)
    {
        NullSlots = new List<ISlot>();
        UnitSize = maxSize;
        Slots = new List<ISlot>();
        for (int i = 0; i < maxSize; i++)
        {
            Slots.Add(new Slot(this));
        }
    }
}