using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 画面全体を覆うフェード演出クラス
/// </summary>
public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    public Canvas FadeCanvas;

    [SerializeField]
    public Image FadeImage;

    [Range(0.01f, 0.1f)]
    public float FadeSpeed = 0.01f;

    private readonly float maxOpacity = 1.0f;
    private bool isFade;

    /// <summary>
    /// 生成時
    /// </summary>
    private void Awake()
    {
        if (FadeCanvas != null)
        {
            FadeCanvas.gameObject.SetActive(false);
        }

        if (FadeImage != null)
		{
            FadeImage.rectTransform.sizeDelta = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
            FadeImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		}
    }

    /// <summary>
    /// フェードイン開始
    /// </summary>
    public void FadeIn()
    {
        StartCoroutine("FadeInCoroutine");
    }

    /// <summary>
    /// フェードインのコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeInCoroutine()
    {
        if (FadeCanvas == null)
        {
            yield break;
        }

        if (FadeImage == null)
        {
            yield break;
        }

        isFade = true;

        FadeCanvas.gameObject.SetActive(true);

        while (FadeImage.color.a < maxOpacity)
        {
            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, FadeImage.color.a + FadeSpeed);
            yield return null;
        }

        isFade = false;

        yield break;
    }

    /// <summary>
    /// フェードアウト開始
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine("FadeOutCoroutine");
    }

    /// <summary>
    /// フェードアウトのコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOutCoroutine()
    {
        if (FadeCanvas == null)
        {
            yield break;
        }

        if (FadeImage == null)
        {
            yield break;
        }

        while (isFade)
        {
            yield return null;
        }

        while (FadeImage.color.a > 0.0f)
        {
            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, FadeImage.color.a - FadeSpeed);
            yield return null;
        }

        FadeCanvas.gameObject.SetActive(false);

        yield break;
    }
}
