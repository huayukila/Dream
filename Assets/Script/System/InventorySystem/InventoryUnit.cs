using System;
using System.Collections.Generic;

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
    Guid ID { get; }
    int UnitSize { get; }
    List<ISlot> Slots { get; }
    List<ISlot> NullSlots { get; }
}

public class InventoryUnit : IInventoryUnit
{
    public Guid ID { get; private set; }
    public int UnitSize { get; }
    public List<ISlot> Slots { get; }
    public List<ISlot> NullSlots { get; set; }

    public static InventoryUnit CreateStorageUnit(int maxSize, out Guid id)
    {
        id = Guid.NewGuid();
        var unit = new InventoryUnit(maxSize)
        {
            ID = id
        };
        return unit;
    }

    public static InventoryUnit CreateStorageUnitByID(int maxSize, Guid id)
    {
        var unit = new InventoryUnit(maxSize)
        {
            ID = id
        };
        return unit;
    }
    
    public static InventoryUnit CreateStorageUnit(int maxSize)
    {
        var unit = new InventoryUnit(maxSize)
        {
            ID = Guid.NewGuid()
        };
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