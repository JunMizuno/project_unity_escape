using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class TouchUIBase : MonoBehaviour
{
    // @memo. タッチ操作の範囲を表すImageを設定すること
    [SerializeField]
    public Image touchArea;

    private bool isInSight;
    public bool IsInSight
    {
        get
        {
            return isInSight;
        }
    }

    private Vector3 pointerDownPos;
    private Vector3 pointerMovingPos;
    private Vector3 pointerUpPos;

    private readonly float MOVE_MARGIN = 3.0f;

    public void Start()
    {
        
    }

    virtual public void OnPointerEnter()
    {
        isInSight = true;
    }

    virtual public void OnPointerExit()
    {
        isInSight = false;
    }

    virtual public void OnPointerDown()
    {
        pointerUpPos = Vector3.zero;
        pointerDownPos = Input.mousePosition;
    }

    virtual public void OnPointerMove()
    {
        pointerMovingPos = Input.mousePosition;

        Debug.Log(string.Format("GetMoveDistance():{0}", GetMoveDistance()).WithColorTag(Color.cyan));

        GetMoveDirection();
    }

    virtual public void OnPointerUp()
    {
        pointerDownPos = Vector3.zero;
        pointerMovingPos = Vector3.zero;
        pointerUpPos = Input.mousePosition;
    }

    virtual protected void SetTouchAction()
    {

    }

    /// <summary>
    /// コールバックの設定
    /// </summary>
    /// <param name="callback"></param>
    protected void SetPointerDownCallback(UnityAction<BaseEventData> callback)
    {
        SetCallback(callback, EventTriggerType.PointerDown);
    }

    protected void SetPointerMoveCallback(UnityAction<BaseEventData> callback)
    {
        SetCallback(callback, EventTriggerType.Drag);
    }

    protected void SetPointerUpCallback(UnityAction<BaseEventData> callback)
    {
        SetCallback(callback, EventTriggerType.PointerUp);
    }

    protected void SetCallback(UnityAction<BaseEventData> callback, EventTriggerType triggerType)
    {
        if (touchArea == null)
        {
            return;
        }

        EventTrigger trigger = touchArea.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener(callback);
        trigger.triggers.Add(entry);
    }

    /// <summary>
    /// タッチの移動距離を取得する
    /// </summary>
    /// <returns></returns>
    protected float GetMoveDistance()
    {
        if (pointerDownPos == Vector3.zero || pointerMovingPos == Vector3.zero)
        {
            return 0.0f;
        }

        return (pointerDownPos - pointerMovingPos).sqrMagnitude;
    }

    /// <summary>
    /// 移動している向きを取得する
    /// </summary>
    /// <returns></returns>
    protected ControllerDirection.MoveDirection GetMoveDirection()
    {
        ControllerDirection.MoveDirection retValue = ControllerDirection.MoveDirection.Center;

        if (pointerMovingPos == Vector3.zero)
        {
            return retValue;
        }

        var diff = pointerMovingPos - pointerDownPos;

        // @todo. 判定値、再考の必要あり
        // 右
        if (diff.x > 0.0f && diff.y < MOVE_MATGIN && diff.z < MOVE_MATGIN)
        {
            retValue = ControllerDirection.MoveDirection.Right;
        }
        // 左
        else if (diff.x < 0.0f && diff.y < MOVE_MATGIN && diff.z < MOVE_MATGIN)
        {
            retValue = ControllerDirection.MoveDirection.Left;
        }
        // 上
        else if (diff.y > 0.0f && diff.x < MOVE_MATGIN && diff.z < MOVE_MATGIN)
        {
            retValue = ControllerDirection.MoveDirection.TopCenter;
        }
        // 下
        else if (diff.y < 0.0f && diff.x < MOVE_MATGIN && diff.z < MOVE_MATGIN)
        {
            retValue = ControllerDirection.MoveDirection.BottomCenter;
        }
        // 右上
        else if (diff.x > 0.0f && diff.y > 0.0f && diff.z < MOVE_MATGIN)
        {
            retValue = ControllerDirection.MoveDirection.TopRight;
        }
        // 右下
        else if (diff.x > 0.0f && diff.y < 0.0f && diff.z < MOVE_MATGIN)
        {
            retValue = ControllerDirection.MoveDirection.BottomRight;
        }
        // 左上
        else if (diff.x < 0.0f && diff.y > 0.0f && diff.z < MOVE_MATGIN)
        {
            retValue = ControllerDirection.MoveDirection.TopLeft;
        }
        // 左下
        else if (diff.x < 0.0f && diff.y < 0.0f && diff.z < MOVE_MATGIN)
        {
            retValue = ControllerDirection.MoveDirection.BottomLeft;
        }

        return retValue;
    }
}
