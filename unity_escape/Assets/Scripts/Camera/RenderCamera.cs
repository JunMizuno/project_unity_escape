using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// レンダーカメラ
/// ミニマップなどに使用するサブカメラ
/// </summary>
public class RenderCamera : MonoBehaviour
{
    // @todo. 仮でメインカメラと座標を同期させるため
    [SerializeField]
    public Camera MainCamera;

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        if (MainCamera != null)
        {
            this.transform.position = MainCamera.transform.position;
            this.transform.localRotation = MainCamera.transform.localRotation;
        }
    }
}
