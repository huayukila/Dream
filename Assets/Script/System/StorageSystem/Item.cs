public interface IItem
{
    int ID { get; }
    string Name { get; }
    int Price { get; }
    bool IsCanStacking { get; }

    void Use();
}

public class Item : IItem
{
    public int ID { get; }
    public string Name { get; }
    public int Price { get; }
    public bool IsCanStacking { get; }

    public Item(int id, string name, int price, bool isCanStacking=true)
    {
        ID = id;
        Name = name;
        Price = price;
        IsCanStacking = isCanStacking;
    }

    public Item()
    {
        
    }

    public void Use()
    {
        // OnUse();
    }

    // protected abstract void OnUse();
}