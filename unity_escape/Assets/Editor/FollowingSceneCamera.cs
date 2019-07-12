using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class FollowingSceneCamera : EditorWindow
{
    public Camera SceneCamera
    {
        get
        {
            return SceneView.lastActiveSceneView.camera;
        }
    }

    [MenuItem("Window/Camera/FollowingSceneCamera")]
    public static void init()
    {
        var window = FindObjectOfType<FollowingSceneCamera>() ;
        if (window != null)
        {
            window.Close();
        }

        window = CreateInstance<FollowingSceneCamera>();
        window.Show();
    }

    private void OnGUI()
    {
        foreach (var camera in FindObjectsOfType<Camera>())
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(camera.name))
            {
                Undo.RecordObject(camera.transform, "camera");
                camera.transform.position = SceneCamera.transform.position;
                camera.transform.rotation = SceneCamera.transform.rotation;
            }

            if (GUILayout.Button("F", GUILayout.Width(30)))
            {
                Selection.activeGameObject = camera.gameObject;
            }
            GUILayout.EndHorizontal();
        }
    }
}
