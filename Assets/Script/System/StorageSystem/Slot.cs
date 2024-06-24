public interface ISlot
{
    IItem Item { get; set; }
    int Nums { get; set; }
}

public class Slot : ISlot
{
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
}