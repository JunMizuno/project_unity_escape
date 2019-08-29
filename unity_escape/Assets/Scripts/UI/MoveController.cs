using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
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

    private void Start()
    {
        SetTouchAnction();
    }

    public void OnPointerEnter()
    {
        isInSight = true;
    }

    public void OnPointerExit()
    {
        isInSight = false;
    }

    public void OnPointerDown()
    {
        var pos = Input.mousePosition;
    }

    public void OnPointerMove()
    {
        var pos = Input.mousePosition;
    }

    public void OnPointerUp()
    {
        var pos = Input.mousePosition;
    }

    /// <summary>
    /// タッチ時のアクション設定
    /// </summary>
    private void SetTouchAnction()
    {
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

    /// <summary>
    /// コールバックの設定
    /// </summary>
    /// <param name="callback"></param>
    private void SetPointerDownCallback(UnityAction<BaseEventData> callback)
    {
        SetCallback(callback, EventTriggerType.PointerDown);
    }

    private void SetPointerMoveCallback(UnityAction<BaseEventData> callback)
    {
        SetCallback(callback, EventTriggerType.Drag);
    }

    private void SetPointerUpCallback(UnityAction<BaseEventData> callback)
    {
        SetCallback(callback, EventTriggerType.PointerUp);
    }

    private void SetCallback(UnityAction<BaseEventData> callback, EventTriggerType triggerType)
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
}
