// @todo. これはPlayerSettingsのシンボルに変えてしまっても良いかもしれない
#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

/// <summary>
/// MessageLogger拡張クラス
/// </summary>
[CustomEditor(typeof(MessageLogger))]
public class MessageLoggerInspector : Editor
{
    private MessageLogger instance;

    [SerializeField]
    public int fontSize = 20;

    private readonly int fontMinSize = 1;
    private readonly int fontMaxSize = 50;

    private void OnEnable()
    {
        // @memo. targetは処理コードのインスタンス、それをここではMessageLogger型でキャストしている
        this.instance = this.target as MessageLogger;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        this.fontSize = Mathf.Clamp(EditorGUILayout.IntField("Font Size(" + fontMinSize.ToString() + " to " + fontMaxSize.ToString() + ")", this.fontSize), fontMinSize, fontMaxSize);
        if (GUILayout.Button("Set", GUILayout.Width(fontMaxSize)))
        {
            this.Apply();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void Apply()
    {
        int rows = this.instance.gameObject.transform.childCount;
        for (int i = 0; i < rows; i++)
        {
            var rowRectTrans = this.instance.gameObject.transform.GetChild(i) as RectTransform;
            rowRectTrans.GetChild(0).GetComponent<Text>();
            rowRectTrans.GetComponent<HorizontalLayoutGroup>().SetLayoutHorizontal();
            rowRectTrans.GetComponent<HorizontalLayoutGroup>().SetLayoutVertical();
        }

        EditorApplication.delayCall += () =>
        {
            for (int i = 0; i < rows; i++)
            {
                var rowRectTrans = this.instance.gameObject.transform.GetChild(i) as RectTransform;
                rowRectTrans.anchoredPosition = new Vector2(0.0f, -rowRectTrans.rect.height * i);
            }
        };
    }
}

#endif // UNITY_EDITOR