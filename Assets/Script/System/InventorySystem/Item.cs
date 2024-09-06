using UnityEngine;

public interface IItem
{
    int ID { get; }
    string Name { get; }
    int Price { get; }
    bool IsCanStack { get; }
    int MaxStackNums { get; }
    Sprite UIImage { get; }
    string Description { get; }
}

public class Item : IItem
{
    public int ID { get; }
    public string Name { get; }
    public int Price { get; }
    public bool IsCanStack { get; }
    public int MaxStackNums { get; }
    public Sprite UIImage { get; }
    public string Description { get; }

    public Item(ItemConfigObj config)
    {
        ID = config.UID;
        Name = config.Name;
        Price = config.Price;
        IsCanStack = config.IsCanStack;
        MaxStackNums = config.MaxStackNums;
        UIImage = config.UIImage;
        Description = config.Description;
    }

    public Item()
    {
    }
}