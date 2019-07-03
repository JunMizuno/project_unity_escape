using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestTouchAction : TouchActionBase
{
    public override void OnPointerEnter(PointerEventData pointerEventData)
    {
        base.OnPointerEnter(pointerEventData);

        Debug.Log("<color=yellow>" + "------TestTouchAction OnPointerEnter" + "</color>");
    }

    public override void OnPointerExit(PointerEventData pointerEventData)
    {
        base.OnPointerExit(pointerEventData);

        Debug.Log("<color=yellow>" + "------TestTouchAction OnPointerExit" + "</color>");
    }
}
