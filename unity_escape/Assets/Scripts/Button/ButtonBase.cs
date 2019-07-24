using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class ButtonBase : MonoBehaviour
{
    private bool isTouched;
    private float[] buttonScaleRate = { 1.1f, 0.8f, 1.0f };
    private float[] buttonScaleDuration = { 0.05f, 0.05f, 0.025f };

    virtual public void OnTouchButtonAction()
    {
        ButtonActionAnimation();
    }

    private void ButtonActionAnimation()
    {
        if (isTouched)
        {
            return;
        }

        isTouched = true;

        var trans = this.transform;
        var sequence = DOTween.Sequence();
        sequence.Append(trans.DOScale(trans.localScale * buttonScaleRate[0], buttonScaleDuration[0]));
        sequence.Append(trans.DOScale(trans.localScale * buttonScaleRate[1], buttonScaleDuration[1]));
        sequence.Append(trans.DOScale(trans.localScale * buttonScaleRate[2], buttonScaleDuration[2])
            .OnComplete(() =>
            {
                isTouched = false;
                sequence.Kill();
            }));
        sequence.Play();
    }
}
