#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

/// <summary>
/// ヒエラルキーに見易いようにカラーを着ける拡張メソッド
/// </summary>
public static class ColorLineExtend
{
    [InitializeOnLoadMethod]
    private static void StartUp()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instanceIndex, Rect selectionRect)
    {
        var index = (int)(selectionRect.y - 4) / 16;

        if (index % 2 == 0)
        {
            return;
        }

        var pos = selectionRect;
        pos.x = 0;
        pos.xMax = selectionRect.xMax;

        var color = GUI.color;
        GUI.color = new Color(0, 0, 0, 0.1f);
        GUI.Box(pos, string.Empty);
        GUI.color = color;
    }
}

#endif // UNITY_EDITOR
