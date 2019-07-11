using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;
using System;

public class MeshRendererShadowAllOff : EditorWindow
{
    private string textArea;
    private UnityEngine.Object obj;
    private GameObject attachObject;

    [MenuItem("Tools/AutoSetting/MeshRendererShadowAllOff")]
    private static void Open()
    {
        EditorWindow.GetWindow<MeshRendererShadowAllOff>("MeshShadowAllOff");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        this.obj = EditorGUILayout.ObjectField(this.obj, typeof(UnityEngine.Object), true);
        EditorGUILayout.EndHorizontal();
        this.attachObject = (GameObject)this.obj;

        if (GUILayout.Button("子階層のMashRendererの影を一括でOFFにする"))
        {
            var count = 0;
            var outputText = "設定を変更したオブジェクトのリスト" + "\r\n";

            if (this.attachObject == null)
            {
                this.ShowNotification(new GUIContent("オブジェクトをセットして下さい"));
                return;
            }

            List<GameObject> objList = this.attachObject.GetAll();
            foreach (GameObject go in objList)
            {
                if (go.HasComponent<MeshRenderer>())
                {
                    MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
                    meshRenderer.lightProbeUsage = LightProbeUsage.Off;
                    meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
                    meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
                    meshRenderer.receiveShadows = false;

                    outputText += obj.name + "\r\n";
                    count++;
                }
                else if (go.HasComponent<SkinnedMeshRenderer>())
                {
                    SkinnedMeshRenderer skinnedMeshRenderer = go.GetComponent<SkinnedMeshRenderer>();
                    skinnedMeshRenderer.lightProbeUsage = LightProbeUsage.Off;
                    skinnedMeshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
                    skinnedMeshRenderer.shadowCastingMode = ShadowCastingMode.Off;
                    skinnedMeshRenderer.receiveShadows = false;

                    outputText += obj.name + "\r\n";
                    count++;
                }
            }

            this.ShowNotification(new GUIContent(count + "件の設定をOFFにしました"));
            this.textArea = outputText;
            Debug.Log(textArea);
        }
    }
}
