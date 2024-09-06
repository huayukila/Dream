using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemConfigDatabase", fileName = "ItemConfigDatabase")]
public class ItemConfigDatabase : ScriptableObject
{
    [SerializeField] public List<ItemConfigObj> ItemConfigObjs = new List<ItemConfigObj>();
}