using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField]
    public Camera MainCamera;

    [SerializeField]
    public GameObject TargetObject;

    private Vector3 lastTouchPos = Vector3.zero;
    private Vector3 newAngle = Vector3.zero;

    private void Update()
    {
        if (TargetObject == null)
        {
            CalcRotateByMyself();
        }
        else
        {
            CalcRotateWithTarget();
        }
    }

    private void CalcRotateByMyself()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RecordCameraRotateByMyself();
        }
        else if (Input.GetMouseButton(0))
        {
            RefreshCameraRotateByMyself();
        }
    }

    private void RecordCameraRotateByMyself()
    {
        if (MainCamera == null)
        {
            return;
        }

        // @memo. カメラの角度を保持
        newAngle = MainCamera.transform.localEulerAngles;
        lastTouchPos = Input.mousePosition;
    }

    private void RefreshCameraRotateByMyself()
    {
        if (MainCamera == null)
        {
            return;
        }

        // @memo. カメラの角度を更新
        newAngle.y += (Input.mousePosition.x - lastTouchPos.x) * 0.1f;
        newAngle.x -= (Input.mousePosition.y - lastTouchPos.y) * 0.1f;
        MainCamera.transform.localEulerAngles = newAngle;

        lastTouchPos = Input.mousePosition;
    }

    private void CalcRotateWithTarget()
    {

    }

    private void RecordCameraRotateWithTarget()
    {

    }

    private void RefreshCameraRotateWithTarget()
    {

    }
}
