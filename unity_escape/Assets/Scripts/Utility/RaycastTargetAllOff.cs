using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class RaycastTargetAllOff : EditorWindow
{
    private string textArea;
    private UnityEngine.Object obj;
    private GameObject attachObject;

    [MenuItem("Tools/AutoSetting/RaycastTargetAllOff")]
    private static void Open()
    {
        EditorWindow.GetWindow<RaycastTargetAllOff>("RaycastAllOff");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        this.obj = EditorGUILayout.ObjectField(this.obj, typeof(UnityEngine.Object), true);
        EditorGUILayout.EndHorizontal();
        this.attachObject = (GameObject)this.obj;

        if (GUILayout.Button("子階層のRaycastTargetを一括でOFFにする"))
        {
            int count = 0;
            string outputText = "RaycastTargetを変更したオブジェクトのリスト" + "\r\n";

            if (this.attachObject == null)
            {
                this.ShowNotification(new GUIContent("オブジェクトをセットして下さい"));
                return;
            }

            List<GameObject> objList = this.attachObject.GetAll();
            foreach (GameObject go in objList)
            {
                if (go.HasComponent<Image>() && go.GetComponent<Image>().raycastTarget)
                {
                    go.GetComponent<Image>().raycastTarget = false;
                    outputText += go.name + "\r\n";
                    count++;
                }
                else if (go.HasComponent<Text>() && go.GetComponent<Text>().raycastTarget)
                {
                    go.GetComponent<Text>().raycastTarget = false;
                    outputText += go.name + "\r\n";
                    count++;
                }
                // @memo. TextMeshProを使う場合は有効化すること
                /*
                else if (go.HasComponent<TextMeshProUGUI>() && go.GetComponent<TextMeshProUGUI>().raycastTarget)
                {
                    go.GetComponent<TextMeshProUGUI>().raycastTarget = false;
                    outputText += go.name + "\r\n";
                    count++;
                }
                */
            }

            this.ShowNotification(new GUIContent(count + "件のRaycastTargetをOFFにしました"));
            this.textArea = outputText;
            Debug.Log(textArea);
        }
    }
}
