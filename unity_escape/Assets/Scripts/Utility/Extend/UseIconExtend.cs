#if UNITY_EDITOR
using System.Reflection;
using UnityEngine;
using UnityEditor;

public static class UseIconExtend
{
    private const int WIDTH = 16;

    private static readonly MethodInfo getIconForObject = typeof(EditorGUIUtility).GetMethod("GetIconForObject", BindingFlags.NonPublic | BindingFlags.Static);

    [InitializeOnLoadMethod]
    private static void StartUp()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instanceIndex, Rect selectionRect)
    {
        GameObject go = EditorUtility.InstanceIDToObject(instanceIndex) as GameObject;

        if (go == null)
        {
            return;
        }

        var parameters = new object[] { go };
        var icon = getIconForObject.Invoke(null, parameters) as Texture;

        if (icon == null)
        {
            return;
        }

        var pos = selectionRect;
        pos.x = pos.xMax - WIDTH * 2;
        pos.width = WIDTH;

        GUI.DrawTexture(pos, icon, ScaleMode.ScaleToFit, true);
    }
}
#endif