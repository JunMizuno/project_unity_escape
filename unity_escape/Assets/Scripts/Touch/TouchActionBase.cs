using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchActionBase :
    MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IMoveHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    IScrollHandler
{
    /// <summary>
    /// ドラッグ開始時
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        Debug.Log("<color=yellow>" + "------OnBeginDrag" + "</color>");
    }

    /// <summary>
    /// ドラッグ中
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnDrag(PointerEventData pointerEventData)
    {
        Debug.Log("<color=yellow>" + "------OnDrag" + "</color>");
    }

    /// <summary>
    /// ドラッグ終了時
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnEndDrag(PointerEventData pointerEventData)
    {
        Debug.Log("<color=yellow>" + "------OnEndDrag" + "</color>");
    }

    /// <summary>
    /// 不明
    /// </summary>
    /// <param name="axisEventData"></param>
    public void OnMove(AxisEventData axisEventData)
    {
        Debug.Log("<color=yellow>" + "------OnMove" + "</color>");
    }

    /// <summary>
    /// グリック時
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("<color=yellow>" + "------OnPointerDown" + "</color>");
    }

    /// <summary>
    /// クリック終了時
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log("<color=yellow>" + "------OnPointerUp" + "</color>");
    }

    /// <summary>
    /// クリック時
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("<color=yellow>" + "------OnPointerClick" + "</color>");
    }

    /// <summary>
    /// マウスポインタの当たりが発生した瞬間
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("<color=yellow>" + "------OnPointerEnter" + "</color>");
    }

    /// <summary>
    /// マウスポインタの当たりが外れた瞬間
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log("<color=yellow>" + "------OnPointerExit" + "</color>");
    }

    /// <summary>
    /// マウススクロール時
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnScroll(PointerEventData pointerEventData)
    {
        Debug.Log("<color=yellow>" + "------OnScroll" + "</color>");
    }
}
