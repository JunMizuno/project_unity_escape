using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GizmosBase : MonoBehaviour
{
    [SerializeField]
    public Vector3[] TestVector;

    [SerializeField]
    public Transform[] TestTrans;

    [SerializeField]
    public bool IsShow;

    private Vector3 localPos = Vector3.zero;
    private Vector3 gizmosSize = Vector3.zero;
    private Vector3 gizmosRange = Vector3.zero;

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
        if (IsShow)
        {
			DrawWireCube();
			DrawLine();
			DrawRay();
        }
    }

    /// <summary>
    /// ヒエラルキーで選択されている場合
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // @memo. 必要になった場合は実装
    }

    private void DrawCube()
	{

	}

    private void DrawWireCube()
	{
		Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 0.5f);
		gizmosRange.Set(1.0f, 1.0f, 1.0f);

		foreach (var pos in TestVector)
		{
			// @memo. Drawには色々種類がある
			//Gizmos.DrawCube(pos, gizmosRange);
			Gizmos.DrawWireCube(pos, gizmosRange);
		}
	}

	private void DrawLine()
	{
		// @memo. 第1に開始地点、第2に終了地点を指定
		Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
		Gizmos.DrawLine(Vector3.right, Vector3.right + Vector3.forward * 50.0f);
	}

	private void DrawRay()
	{
		// @memo. 第1に開始地点、第2にベクトルを指定
		Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
		Gizmos.DrawRay(Vector3.left, Vector3.back * 50.0f);
	}
}
