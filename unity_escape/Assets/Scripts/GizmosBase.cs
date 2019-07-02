using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GizmosBase : MonoBehaviour
{
    [SerializeField]
    public bool isShow;

    private Vector3 localPos = Vector3.zero;
    private Vector3 gizmosSize = Vector3.zero;

    public void SetLocalPos(Vector3 pos)
    {
        localPos.Set(pos.x, pos.y, pos.z);
    }

    public void SetGizmosSize(Vector3 size)
    {
        gizmosSize.Set(size.x, size.y, size.z);
    }

    /// <summary>
    /// オブジェクトにアタッチされている場合
    /// </summary>
    private void OnDrawGizmos()
    {
        
    }

    /// <summary>
    /// ヒエラルキーで選択されている場合
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        
    }
}
