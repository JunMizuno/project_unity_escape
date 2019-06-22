using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LifeImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale= new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        
    }

    public void ScaleUp(float scale, float duration)
    {
        var trans = this.transform;
        trans.DOScale(new Vector3(scale, scale, scale), duration)
            .SetRelative()
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
            });
    }

    public void ScaleDown(float scale, float duration)
    {
        var trans = this.transform;
        trans.DOScale(new Vector3(scale, scale, scale), duration)
            .SetRecyclable()
            .OnComplete(() =>
            {
            });
    }

    public void ShowLifeImage()
    {
        var trans = this.transform;
        trans.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        this.transform.localScale = trans.localScale;
    }

    public void HideLifeImage()
    {
        var trans = this.transform;
        trans.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        this.transform.localScale = trans.localScale;
    }

    /// <summary>
    /// @detail:スケールが1.0fであるならば「表示している」とします
    /// </summary>
    /// <returns></returns>
    public bool IsShow()
    {
        var trans = this.transform;
        if (trans.localScale.x >= 1.0f && trans.localScale.y >= 1.0f && trans.localScale.z >= 1.0f)
        {
            return true;
        }

        return false;
    }

    public IEnumerator ScaleUpActionWithDelay(float delayCount)
    {
        yield return null;
    }
}
