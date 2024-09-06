using System;
using UnityEngine;

public enum ItemType
{
    Material = 1000, //�f��
    Consumable = 2000, //���Օi
    Equipment = 3000, //����
    Weapon = 4000, //����
    Tool = 5000, //�H��
}

[Serializable]
public struct ItemConfigObj
{
    [SerializeField] public int UID;
    [SerializeField] public string Name;
    [SerializeField] public ItemType Type;
    [SerializeField] public int Price;
    [SerializeField] public bool IsCanStack;
    [SerializeField] public int MaxStackNums;
    [SerializeField] public Sprite UIImage;
    [SerializeField] public string Description;
    [SerializeField] public GameObject Model;
}