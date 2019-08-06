using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ImageEffectShaderを利用したフェードアウトのサンプル
/// @memo. カメラにアタッチする
/// @memo. SampleImageEffectシェーダーのマテリアルをアタッチすること
/// </summary>
public class ImageEffectSample : MonoBehaviour
{
    [SerializeField]
    public Material material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
