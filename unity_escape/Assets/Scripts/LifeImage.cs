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
        ScaleUp(1.0f, 1.0f);
    }

    private void OnDisable()
    {
        
    }

    private void ScaleUp(float scale, float duration)
    {
        var trans = this.transform;
        trans.DOScale(new Vector3(scale, scale, scale), duration)
            .SetRelative()
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
            });
    }

    private void ScaleDown(float scale, float duration)
    {
        var trans = this.transform;
        trans.DOScale(new Vector3(scale, scale, scale), duration);
    }
}
