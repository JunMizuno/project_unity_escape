using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class MoveController : TouchUIBase
{
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

        });

        SetPointerUpCallback((baseEventData) =>
        {

        });
    }
}
