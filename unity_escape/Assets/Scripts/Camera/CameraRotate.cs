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

    readonly float moveVelocity = 2.0f;

    // @memo. 以下、参考サイトそのまま抜粋
    private float distance = 4.0f;              // 追跡しているオブジェクトとの距離
    private float polarAngle = 25.0f;           // 極地角の初期値
    private float azimuthalAngle = 0.0f;        // 全方位角の初期値
    private float minDistance = 1.0f;
    private float maxDistance = 7.0f;
    private float minPolarAngle = 5.0f;
    private float maxPolarAngle = 75.0f;
    private float mouseXSensitivity = 5.0f;
    private float mouseYSensitivity = 5.0f;
    private float scrollSensitivity = 5.0f;

    private void Awake()
    {
        if (TargetObject == null)
        {

        }
        else
        {
            if (MainCamera != null)
            {
                MainCamera.transform.LookAt(TargetObject.transform);
            }
        }
    }

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
        if (MainCamera == null)
        {
            return;
        }

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
        // @memo. カメラの角度を保持
        newAngle = MainCamera.transform.localEulerAngles;
        lastTouchPos = Input.mousePosition;
    }

    private void RefreshCameraRotateByMyself()
    {
        // @memo. カメラの角度を更新
        newAngle.y += (Input.mousePosition.x - lastTouchPos.x) * 0.1f;
        newAngle.x -= (Input.mousePosition.y - lastTouchPos.y) * 0.1f;
        MainCamera.transform.localEulerAngles = newAngle;

        lastTouchPos = Input.mousePosition;
    }

    private void CalcRotateWithTarget()
    {
        if (MainCamera == null)
        {
            return;
        }

        if (TargetObject == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (Input.GetMouseButton(0))
        {
            RefreshCameraRotateWithTarget();
        }

        RefreshDistanceFromTarget();
        RefreshLookAtTargetPos();
    }

    private void RefreshCameraRotateWithTarget()
    {
        float rotateX = Input.GetAxis("Mouse X") * moveVelocity;
        float rotateY = Input.GetAxis("Mouse Y") * moveVelocity;

        rotateX = azimuthalAngle - (rotateX * mouseXSensitivity);
        azimuthalAngle = Mathf.Repeat(rotateX, 360.0f);

        rotateY = polarAngle + (rotateY * mouseYSensitivity);
        polarAngle = Mathf.Clamp(rotateY, minPolarAngle, maxPolarAngle);
    }

    private void RefreshDistanceFromTarget()
    {
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");

        scrollValue = distance - (scrollValue * scrollSensitivity);
        distance = Mathf.Clamp(scrollValue, minDistance, maxDistance);
    }

    private void RefreshLookAtTargetPos()
    {
        var lookAtPos = TargetObject.transform.position;
        var da = azimuthalAngle * Mathf.Deg2Rad;
        var dp = polarAngle * Mathf.Deg2Rad;

        float x = lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da);
        float y = lookAtPos.y + distance * Mathf.Cos(dp);
        float z = lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da);

        MainCamera.transform.position = new Vector3(x, y, z);
        MainCamera.transform.LookAt(lookAtPos);
    }
}
