using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// オブジェクトに対してのタッチ判定取得クラス
/// 判定を撮りたいオブジェクトに対してアタッチすること
/// ヒエラルキーにEventSystemの生成必須
/// 対象のカメラ側にPhysicsRaycasterのアタッチ必須
/// </summary>
public abstract class TouchActionBase :
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
    public virtual void OnBeginDrag(PointerEventData pointerEventData)
    {

    }

    /// <summary>
    /// ドラッグ中
    /// </summary>
    /// <param name="pointerEventData"></param>
    public virtual void OnDrag(PointerEventData pointerEventData)
    {

    }

    /// <summary>
    /// ドラッグ終了時
    /// </summary>
    /// <param name="pointerEventData"></param>
    public virtual void OnEndDrag(PointerEventData pointerEventData)
    {

    }

    /// <summary>
    /// 不明
    /// </summary>
    /// <param name="axisEventData"></param>
    public virtual void OnMove(AxisEventData axisEventData)
    {

    }

    /// <summary>
    /// グリック時(クリック直後)
    /// </summary>
    /// <param name="pointerEventData"></param>
    public virtual void OnPointerDown(PointerEventData pointerEventData)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {

        }
    }

    /// <summary>
    /// クリック終了時
    /// </summary>
    /// <param name="pointerEventData"></param>
    public virtual void OnPointerUp(PointerEventData pointerEventData)
    {

    }

    /// <summary>
    /// クリック時(OnPointerUpと同じ挙動)
    /// </summary>
    /// <param name="pointerEventData"></param>
    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {

        }
    }

    /// <summary>
    /// マウスポインタの当たりが発生した瞬間
    /// </summary>
    /// <param name="pointerEventData"></param>
    public virtual void OnPointerEnter(PointerEventData pointerEventData)
    {

    }

    /// <summary>
    /// マウスポインタの当たりが外れた瞬間
    /// </summary>
    /// <param name="pointerEventData"></param>
    public virtual void OnPointerExit(PointerEventData pointerEventData)
    {

    }

    /// <summary>
    /// マウススクロール時
    /// </summary>
    /// <param name="pointerEventData"></param>
    public virtual void OnScroll(PointerEventData pointerEventData)
    {

    }
}
