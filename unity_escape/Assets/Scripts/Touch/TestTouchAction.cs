using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestTouchAction : TouchActionBase
{
    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);

        Debug.Log("------TestTouchAction OnPointerClick".WithColorTag(Color.green));
    }

    public override void OnPointerDown(PointerEventData pointerEventData)
    {
        base.OnPointerDown(pointerEventData);

        Debug.Log("------TestTouchAction OnPointerDown".WithColorTag(Color.green));
    }

    public override void OnPointerEnter(PointerEventData pointerEventData)
    {
        base.OnPointerEnter(pointerEventData);

        Debug.Log("------TestTouchAction OnPointerEnter".WithColorTag(Color.yellow));
    }

    public override void OnPointerExit(PointerEventData pointerEventData)
    {
        base.OnPointerExit(pointerEventData);

        Debug.Log("------TestTouchAction OnPointerExit".WithColorTag(Color.yellow));
    }
}
