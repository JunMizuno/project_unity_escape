using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ImageEffectShaderを利用する為のクラス
/// @memo. カメラにアタッチする
/// @memo. SampleImageEffectシェーダーのマテリアルをアタッチすること
/// </summary>
public class ImageEffectBase : MonoBehaviour
{
    [SerializeField]
    public Material material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material == null)
        {
            return;
        }

        StandardEffect(source, destination);
    }

    private void StandardEffect(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
