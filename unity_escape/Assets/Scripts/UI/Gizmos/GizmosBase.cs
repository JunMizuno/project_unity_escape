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
	public Texture Tex;

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

    private void DrawSphere()
	{

	}

    private void DrawWireSphere()
	{
        // @memo. 第1に中心座標、第2に半径を指定
		Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 0.5f);
		var pos = Vector3.zero;
		Gizmos.DrawWireSphere(pos, 1.0f);
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

    private void DrawFrustum()
	{
		// @memo. 第1に表示する座標、第2にカメラ視野、第3に到達点、第4にカメラの座標までの距離、第5に幅に対する高さの比率を指定
		Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        Gizmos.DrawFrustum(Vector3.zero, 60.0f, 1000.0f, 0.0f, 1.6f);
	}

	private void DrawTexture()
	{
        if (Tex == null)
        {
            return;
        }

        // @memo. テクスチャサイズを偶数で指定
		Vector2 textureSize = new Vector2(2.0f, 2.0f);
		Gizmos.DrawGUITexture(new Rect(-textureSize / 2.0f, textureSize), Tex);
	}
}
