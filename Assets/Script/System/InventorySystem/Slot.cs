using Framework;

public interface ISlot
{
    IInventoryUnit ParentInventoryUnit { get; }

    IItem Item { get; set; }

    int Nums { get; set; }
    // void InitItem(ItemConfigObj config);
}

public class Slot : ISlot
{
    public IInventoryUnit ParentInventoryUnit { get; private set; }
    public IItem Item { get; set; } = null;
    private int m_Nums = 0;

    public int Nums
    {
        get => m_Nums;
        set
        {
            m_Nums = value;
            if (value <= 0)
            {
                m_Nums = 0;
                Item = null;
            }
        }
    }

    // public void InitItem(ItemConfigObj config)
    // {
    //     Item ??= new Item(config);
    // }

    public void ChangeParentInventoryUnit(IInventoryUnit unit)
    {
        ParentInventoryUnit = unit;
    }

    public Slot(IInventoryUnit parentUnit)
    {
        ChangeParentInventoryUnit(parentUnit);
    }
}