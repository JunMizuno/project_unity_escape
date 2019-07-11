using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// @todo. 機能面は後ほど確認する
/// </summary>
public static class GameObjectExtend
{
    public static bool HasComponent<T>(this GameObject self) where T : Component
    {
        return self.GetComponent<T>() != null;
    }

    public static void GetChildren(GameObject obj, ref List<GameObject> allChildren)
    {
        Transform children = obj.GetComponentInChildren<Transform>();

        if (children.childCount == 0)
        {
            return;
        }

        foreach (Transform child in children)
        {
            allChildren.Add(child.gameObject);
            GameObjectExtend.GetChildren(child.gameObject, ref allChildren);
        }
    }

    public static List<GameObject> GetAll(this GameObject self)
    {
        var allChildren = new List<GameObject>();
        allChildren.Add(self);
        GameObjectExtend.GetChildren(self, ref allChildren);
        return allChildren;
    }

}
