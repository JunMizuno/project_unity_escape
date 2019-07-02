using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TouchManager
{
    public static void OnTouchAction()
    {
        if (Application.isEditor)
        {
            EditorTouchAction();
        }
        else
        {
            DeviceTouchAction();
        }
    }

    private static void EditorTouchAction()
    {

    }

    private static void DeviceTouchAction()
    {

    }
}
