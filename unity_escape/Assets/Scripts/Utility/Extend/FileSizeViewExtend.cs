#if UNITY_EDITOR

using System.IO;
using UnityEngine;
using UnityEditor;

/// <summary>
/// アセットのアイコン上部にサイズを表示する拡張メソッド
/// </summary>
public static class FileSizeViewExtend
{
    private const string REMOVE_STR = "Assets";

    private static readonly int removeCount = REMOVE_STR.Length;
    private static readonly Color specifiedColor = new Color(0.635f, 0.635f, 0.635f, 1.0f);

    [InitializeOnLoadMethod]
    private static void StartUp()
    {
        EditorApplication.projectWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(string guid, Rect selectionRect)
    {
        var dataPath = Application.dataPath;
        var startIndex = dataPath.LastIndexOf(REMOVE_STR, System.StringComparison.CurrentCulture);
        var dir = dataPath.Remove(startIndex, removeCount);
        var path = dir + AssetDatabase.GUIDToAssetPath(guid);

        if (!File.Exists(path))
        {
            return;
        }

        var fileInfo = new FileInfo(path);
        var fileSize = fileInfo.Length;
        var text = GetFormatSizeString((int)fileSize);

        var label = EditorStyles.label;
        var content = new GUIContent(text);
        var width = label.CalcSize(content).x;

        var pos = selectionRect;
        pos.x = pos.xMax - width;
        pos.width = width;
        pos.yMin++;

        var color = GUI.color;
        GUI.color = specifiedColor;
        //GUI.DrawTexture(pos, EditorGUIUtility.whiteTexture);
        GUI.color = color;
        GUI.Label(pos, text);
    }

    private static string GetFormatSizeString(int size)
    {
        return GetFormatSizeString(size, 1024);
    }

    private static string GetFormatSizeString(int size, int p)
    {
        return GetFormatSizeString(size, p, "#,##0.##");
    }

    private static string GetFormatSizeString(int size, int p, string specifier)
    {
        var suffix = new[] { "", "K", "M", "G", "T", "P", "E", "Z", "Y" };
        int index = 0;

        while (size >= p)
        {
            size /= p;
            index++;
        }

        return string.Format("{0}{1}B", size.ToString(specifier), (index < suffix.Length) ? suffix[index] : "");
    }
}

#endif // UNITY_EDITOR
