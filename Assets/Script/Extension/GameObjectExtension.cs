using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class GameObjectExtension
{
    public static int GetChildCountExtension(this Component obj)
    {
        if (obj == null)
        {
            return 0;
        }

        return obj.transform.childCount;
    }

    public static int GetChildCountExtension(this GameObject obj)
    {
        if (obj == null)
        {
            return 0;
        }

        return obj.transform.childCount;
    }
    
    public static bool HasComponent<T>(this GameObject go, bool checkChildren)where T: Component
    {
        if (!checkChildren)
        {
            return go.GetComponent<T>();
        }
        else
        {
            return go.GetComponentsInChildren<T>().FirstOrDefault() != null;
        }
    }

    public static bool HasComponent(this GameObject go, Type type, bool checkChildren)
    {
        if (!checkChildren)
        {
            return go.GetComponent(type) != null;
        }
        else
        {
            return go.GetComponentsInChildren(type).FirstOrDefault() != null;
        }
    }
}