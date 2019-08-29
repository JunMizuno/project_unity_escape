using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 伸縮するゲージクラス
/// FillMethodはインスペクター側で指定する仕様
/// </summary>
public class Gauge : MonoBehaviour
{
    private Image gauge;
    private float appearSeconds = 1.0f;
    public float AppearSeconds
    {
        set { appearSeconds = value; }
    }

    private void Awake()
    {
        gauge = this.GetComponent<Image>();
    }

    private void Start()
    {
        if (gauge == null)
        {
            return;
        }

        SetInitialValueForFillAmount(0.0f);
    }

    /// <summary>
    /// FillAmountの初期値を設定
    /// </summary>
    /// <param name="value"></param>
    public void SetInitialValueForFillAmount(float value)
    {
        if (gauge != null)
        {
            gauge.fillAmount = Mathf.Clamp(value, 0.0f, 1.0f);
        }
    }

    /// <summary>
    /// ゲージの状態を変更
    /// </summary>
    /// <param name="value"></param>
    public void RefreshGaugeStateByCoroutine(float value)
    {
        if (gauge == null)
        {
            return;
        }

        StartCoroutine(RefreshGaugeFillAmount(Mathf.Clamp(value, 0.0f, 1.0f)));
    }

    /// <summary>
    /// 引数の値に応じてゲージを更新
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private IEnumerator RefreshGaugeFillAmount(float value)
    {
        float startedFillAmountValue = gauge.fillAmount;
        float sign = (startedFillAmountValue > value) ? -1.0f : 1.0f;
        float addAmountValue = 0.0f;

        float totalValue = Mathf.Abs(startedFillAmountValue - value);
        while (totalValue > Mathf.Abs(addAmountValue))
        {
            // @memo. 1秒間に増加させる値に対して1フレームの秒数を掛け合わせてそのフレーム分の増加値を計算
            addAmountValue += ((totalValue / appearSeconds) * Time.deltaTime) * sign;
            gauge.fillAmount = startedFillAmountValue + addAmountValue;
            yield return null;
        }

        gauge.fillAmount = value;

        yield break;
    }

    /// <summary>
    /// ゲージの状態を変更
    /// </summary>
    /// <param name="value"></param>
    /// <param name="ease"></param>
    public void RefreshGaugeStateByDOTween(float value, Ease ease = Ease.OutSine)
    {
        if (gauge == null)
        {
            return;
        }

        RefreshGaugeFillAmount(Mathf.Clamp(value, 0.0f, 1.0f), ease);
    }

    /// <summary>
    /// 引数の値に応じてゲージを更新
    /// </summary>
    /// <param name="value"></param>
    /// <param name="ease"></param>
    private void RefreshGaugeFillAmount(float value, Ease ease)
    {
        float startedFillAmount = gauge.fillAmount;
        float sign = (startedFillAmount > value) ? -1.0f : 1.0f;

        // @memo. ボックス化はパフォーマンスが悪いとの事で、それを防ぐ書き方
        // @memo. ラムダ、開始値、終了値、所要時間(秒)の順に指定
        DOTween.To(updateValue =>
        {
            gauge.fillAmount = updateValue;
        },
        startedFillAmount,
        (value * sign),
        appearSeconds).Play();
    }
}
