using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// TouchAreaと合わせてアタッチして使用する仕様
/// ゲームシーンに対してのマネージャー、UIには個別で対応すること
/// </summary>
public class TouchManager : TouchActionBase
{
    [SerializeField]
    public GameObject FieldObject;

    private Action<PointerEventData> pointerEnterAction;
    public Action<PointerEventData> PointerEnterAction
    {
        set { pointerEnterAction = value; }
    }
    private Action<PointerEventData> pointerExitAction;
    public Action<PointerEventData> PointerExitAction
    {
        set { pointerExitAction = value; }
    }
    private Action<PointerEventData> touchDownAction;
    public Action<PointerEventData> TouchDownAction
    {
        set { touchDownAction = value; }
    }
    private Action touchHoldAction;
    public Action TouchHoldAction
    {
        set { touchHoldAction = value; }
    }
    public Action stopHoldAction;
    public Action StopHoldAction
    {
        set { stopHoldAction = value; }
    }
    private Action<PointerEventData> touchUpAction;
    public Action<PointerEventData> TouchUpAction
    {
        set { touchUpAction = value; }
    }

    private Vector2 pointerPositionDelta;
    private Vector2 pointerMovedPosition;

    private Vector2 touchPositionDelta;
    private Vector2 touchActionPosition;

    private bool isTouched;
    private readonly float AVAIRABLE_HOLD_TIME = 1.0f;
    private float currentHoldTime;

    private readonly float PERSPECTIVE_Z_POINT = 5.0f;
    //private readonly float ORTHOGRAPHIC_Z_POINT = 0.0f;

    public override void OnPointerEnter(PointerEventData pointerEventData)
    {
        base.OnPointerEnter(pointerEventData);
        ExecutePointerEnterAction(pointerEventData);
    }

    public override void OnPointerExit(PointerEventData pointerEventData)
    {
        base.OnPointerExit(pointerEventData);
        ExecutePointerExitAction(pointerEventData);
    }

    public override void OnPointerDown(PointerEventData pointerEventData)
    {
        isTouched = true;
        base.OnPointerDown(pointerEventData);
        ExecuteTouchDownAction(pointerEventData);
    }

    public override void OnPointerUp(PointerEventData pointerEventData)
    {
        isTouched = false;
        currentHoldTime = 0.0f;
        base.OnPointerUp(pointerEventData);
        ExecuteTouchUpAction(pointerEventData);
        ExecuteStopHoldAction();
    }

    public override void OnDrag(PointerEventData pointerEventData)
    {
        isTouched = false;
        currentHoldTime = 0.0f;
        base.OnDrag(pointerEventData);
        ExecuteStopHoldAction();
    }

    public override void OnBeginDrag(PointerEventData pointerEventData)
    {
        isTouched = true;
        base.OnBeginDrag(pointerEventData);
    }

    public override void OnEndDrag(PointerEventData pointerEventData)
    {
        isTouched = false;
        currentHoldTime = 0.0f;
        base.OnEndDrag(pointerEventData);
        ExecuteStopHoldAction();
    }

    private void ClearPointerEnterAction()
    {
        this.pointerEnterAction = null;
    }

    private void ClearPointerExitAction()
    {
        this.pointerExitAction = null;
    }

    private void ClearTouchDownAction()
    {
        this.touchDownAction = null;
    }

    private void ClearTouchHoldAction()
    {
        this.touchHoldAction = null;
    }

    private void ClearTouchUpAction()
    {
        this.touchUpAction = null;
    }

    public void ClearAllAction()
    {
        ClearPointerEnterAction();
        ClearPointerExitAction();
        ClearTouchDownAction();
        ClearTouchHoldAction();
        ClearTouchUpAction();
        isTouched = false;
        currentHoldTime = 0.0f;
    }

    public void ExecutePointerEnterAction(PointerEventData pointerEventData)
    {
        this.pointerPositionDelta = pointerEventData.position - this.pointerMovedPosition;
        this.pointerMovedPosition = pointerEventData.position;
        this.pointerEnterAction.NullSafeCall(pointerEventData);
    }

    public void ExecutePointerExitAction(PointerEventData pointerEventData)
    {
        this.pointerPositionDelta = pointerEventData.position - this.pointerMovedPosition;
        this.pointerMovedPosition = pointerEventData.position;
        this.pointerExitAction.NullSafeCall(pointerEventData);
    }

    public void ExecuteTouchDownAction(PointerEventData pointerEventData)
    {
        this.touchDownAction.NullSafeCall(pointerEventData);
    }

    public void ExecuteTouchHoldAction()
    {
        this.touchHoldAction.NullSafeCall();
    }

    public void ExecuteStopHoldAction()
    {
        this.stopHoldAction.NullSafeCall();
    }

    public void ExecuteTouchUpAction(PointerEventData pointerEventData)
    {
        // @memo. タッチが終了した時点でのみ座標の記録を行う
        this.touchPositionDelta = pointerEventData.position - this.touchActionPosition;
        this.touchActionPosition = pointerEventData.position;

        this.touchUpAction.NullSafeCall(pointerEventData);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if (isTouched)
        {
            currentHoldTime += Time.deltaTime;
            if (currentHoldTime >= AVAIRABLE_HOLD_TIME)
            {
                ExecuteTouchHoldAction();
            }
        }
    }

    /// <summary>
    /// タッチ位置のワールド座標を取得
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCurrentTouchActionPositionInWorld()
    {
        // @memo. カメラのPerspectiveとOrthographicで渡す値を変えること
        var position = new Vector3(this.touchActionPosition.x, this.touchActionPosition.y, PERSPECTIVE_Z_POINT);
        var worldPosition = Camera.main.ScreenToWorldPoint(position);

        // @memo. フィールドの設定がある場合はその地面の座標を計算する(フィールドのオブジェクトにはCollider必須)
        // @memo. フィールドの設定が無い場合はワールドのX軸・Y軸のみ返すことになる
        if (FieldObject != null)
        {
            Ray touchPointToRay = Camera.main.ScreenPointToRay(this.touchActionPosition);

            // @memo. 一番近い当たりを取る場合はPhysics.Raycast(ray, out hitInfo)で取れる
            foreach (RaycastHit hitInfo in Physics.RaycastAll(touchPointToRay))
            {
                if (hitInfo.collider.name == FieldObject.name)
                {
                    worldPosition = hitInfo.point;
                    break;
                }
            }
        }

        return worldPosition;
    }

    public float GetDeltaDistance()
    {
        var currenPosition = new Vector3(this.touchActionPosition.x, this.touchActionPosition.y, PERSPECTIVE_Z_POINT);
        var currentWorldPosition = Camera.main.ScreenToWorldPoint(currenPosition);

        var previousPosition = new Vector3(this.touchActionPosition.x - touchPositionDelta.x, this.touchActionPosition.y - touchPositionDelta.y, PERSPECTIVE_Z_POINT);
        var previousWorldPosition = Camera.main.ScreenToWorldPoint(previousPosition);

        return (currentWorldPosition - previousWorldPosition).sqrMagnitude;
    }
}
