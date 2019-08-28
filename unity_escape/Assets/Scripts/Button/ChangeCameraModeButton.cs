using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCameraModeButton : ButtonBase
{
    [SerializeField]
    public Text text;

    private void Start()
    {
        if (text == null)
        {
            text = this.transform.GetChild(0).GetComponent<Text>();
        }

        ChangeText();
    }

    public new void OnTouchButtonAction()
    {
        base.OnTouchButtonAction();

        ChangeCameraMode();
        ChangeText();
    }

    private void ChangeCameraMode()
    {
        if (CameraManager.Instance == null)
        {
            return;
        }

        if (CameraManager.Instance.CurrentViewMode == CameraMode.ViewMode.Global)
        {
            CameraManager.Instance.CurrentViewMode = CameraMode.ViewMode.Following;
        }
        else if (CameraManager.Instance.CurrentViewMode == CameraMode.ViewMode.Following)
        {
            CameraManager.Instance.CurrentViewMode = CameraMode.ViewMode.Global;
        }
    }

    private void ChangeText()
    {
        if (text == null)
        {
            return;
        }

        if (CameraManager.Instance == null)
        {
            return;
        }

        if (CameraManager.Instance.CurrentViewMode == CameraMode.ViewMode.Global)
        {
            text.text = "見下ろし";
        }
        else if (CameraManager.Instance.CurrentViewMode == CameraMode.ViewMode.Following)
        {
            text.text = "追従";
        }
    }
}
