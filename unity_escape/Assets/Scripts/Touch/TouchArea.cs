using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タッチアクションの判定エリアを設定するクラス
/// UIのImageをセットしてそれにアタッチする仕様
/// 通常は画面一杯をカバーする想定
/// </summary>
public class TouchArea : MonoBehaviour
{
    private void Awake()
    {
        var rectTrans = this.GetComponent<RectTransform>();
        if (rectTrans != null)
        {
            rectTrans.sizeDelta = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        }

        var image = this.GetComponent<Image>();
        if (image != null)
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
}
