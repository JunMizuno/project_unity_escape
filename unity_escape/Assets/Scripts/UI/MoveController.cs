using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class MoveController : TouchUIBase
{
    [SerializeField]
    public Image Stick;

    private Sequence sticksSequence;

    private new void Start()
    {
        base.Start();

        SetTouchAnction();
    }

    public new void OnPointerDown()
    {
        base.OnPointerDown();
    }

    public new void OnPointerMove()
    {
        base.OnPointerMove();
    }

    public new void OnPointerUp()
    {
        base.OnPointerUp();
    }

    /// <summary>
    /// タッチ時のアクション設定
    /// </summary>
    protected void SetTouchAnction()
    {
        base.SetTouchAction();

        SetPointerDownCallback((baseEventData) =>
        {

        });

        SetPointerMoveCallback((baseEventData) =>
        {
            var arrivalPos = this.transform.position;
            arrivalPos = new Vector3(arrivalPos.x, arrivalPos.y + 5.0f, arrivalPos.z);
            SetSticksPosition(arrivalPos);
        });

        SetPointerUpCallback((baseEventData) =>
        {
            ClearSticksPosition();
        });
    }

    private void SetSticksPosition(Vector3 arrivalPosition)
    {
        if (Stick == null)
        {
            return;
        }

        var sticksTrans = Stick.transform;



        Stick.transform.localPosition = new Vector3(sticksTrans.localPosition.x, sticksTrans.localPosition.y + 2.0f, sticksTrans.localPosition.z);
        return;


        //sticksSequence.Kill();

        sticksSequence.Append(sticksTrans.DOMove(arrivalPosition, 1.0f));
        sticksSequence.Play();
    }

    /// <summary>
    /// スティック画像の座座標を初期位置に戻す
    /// </summary>
    private void ClearSticksPosition()
    {
        if (Stick == null)
        {
            return;
        }

        var sticksTrans = Stick.transform;

        sticksSequence = DOTween.Sequence();
        sticksSequence.Append(sticksTrans.DOLocalMove(Vector3.zero, 0.2f)
            .SetEase(Ease.Linear));
        sticksSequence.OnComplete(() =>
        {
            sticksSequence.Kill();
        });
        sticksSequence.Play();
    }
}
