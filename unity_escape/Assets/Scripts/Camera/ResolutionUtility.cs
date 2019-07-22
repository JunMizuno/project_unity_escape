using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの視認サイズを調整
/// カメラにアタッチして使う
/// </summary>
[RequireComponent(typeof(Camera))]
public class ResolutionUtility : MonoBehaviour
{
    private new Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();

        // 開発している画面を元に縦横比を取得
        float developAspect = 750.0f / 1334.0f;

        // 実機の画面サイズを参照して縦横比を取得
        float deviceAspect = (float)Screen.width / (float)Screen.height;

        // 実機と開発画面の対比
        float scale = deviceAspect / developAspect;

        // スケールの逆数
        float deviceScale = 1.0f / scale;

        // カメラに設定していたorthographicSizeを実機との対比で計算し直す
        camera.orthographicSize = camera.orthographicSize * deviceScale;

        // リサイズ処理後に自身を破棄
        Destroy(this);
    }
}
