using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : PlayerBase
{
    // 数値を増やすとスピードダウン
    private readonly float MOVING_DISTANCE_PER_SECOND = 0.1f;
    private readonly float MOVING_MIN_TIME = 2.0f;
    private readonly float MOVING_MAX_TIME = 8.0f;
    private readonly float ADJUST_ANGLE_Y = -90.0f;
    private readonly float TURN_AROUND_TIME = 0.1f;

    protected override void Start()
    {
        base.Start();

        SetTouchedAction();
    }

    public void SetTouchedAction()
    {
        var touchArea = GameManager.Instance.GetTouchManager();
        if (touchArea == null)
        {
            return;
        }

        // @todo. カメラモードによって切り替えられるようにする必要あり
        if (true)
        {
            AutoMoving(touchArea);
        }
        else
        {
            ManualMoving(touchArea);
        }
    }

    /// <summary>
    /// タッチポイントまで自動で移動
    /// </summary>
    /// <param name="touchManager"></param>
    private void AutoMoving(TouchManager touchManager)
    {
        touchManager.TouchUpAction = (x) =>
        {
            // @todo. フィールドにマーカーを追加

            var touchWorldPosition = touchManager.GetCurrentTouchActionPositionInWorld();

            // @todo. 物理演算で重力をかけた場合、常に当たりが発生しているので調整の必要あり
            touchWorldPosition.y = 0.0f;

            float distance = (touchWorldPosition - this.transform.position).sqrMagnitude;
            float time = Mathf.Clamp((distance * MOVING_DISTANCE_PER_SECOND), MOVING_MIN_TIME, MOVING_MAX_TIME);

            float dx = touchWorldPosition.x - this.transform.position.x;
            float dz = touchWorldPosition.z - this.transform.position.z;
            float rad = Mathf.Atan2(dz, dx);
            float angle = rad * Mathf.Rad2Deg;

            // @memo. プロジェクトではキャラがZ軸に対して正面を向いている状態がゼロ度となっているため調整
            float newAngleY = (angle + ADJUST_ANGLE_Y) * -1.0f;
            Vector3 newAngles = new Vector3(this.transform.localEulerAngles.x, newAngleY, this.transform.localEulerAngles.z);

            RotatePlayer(newAngles, TURN_AROUND_TIME);

            touchWorldPosition.y = this.transform.position.y;
            MovePlayer(touchWorldPosition, time);
        };
    }

    /// <summary>
    /// タッチしている間のみ移動
    /// </summary>
    /// <param name="touchManager"></param>
    private void ManualMoving(TouchManager touchManager)
    {
        touchManager.TouchHoldAction = () =>
        {
            // @todo. 移動処理
            var currentPos = this.transform.position;
            var reachedPos = new Vector3(currentPos.x, currentPos.y, currentPos.z + 2.0f);

            float distance = (reachedPos - currentPos).sqrMagnitude;
            float time = Mathf.Clamp((distance * MOVING_DISTANCE_PER_SECOND), MOVING_MIN_TIME, MOVING_MAX_TIME);

            float dx = reachedPos.x - currentPos.x;
            float dz = reachedPos.z - currentPos.z;
            float rad = Mathf.Atan2(dz, dx);
            float angle = rad * Mathf.Rad2Deg;

            // @memo. プロジェクトではキャラがZ軸に対して正面を向いている状態がゼロ度となっているため調整
            float newAngleY = (angle + ADJUST_ANGLE_Y) * -1.0f;
            Vector3 newAngles = new Vector3(this.transform.localEulerAngles.x, newAngleY, this.transform.localEulerAngles.z);

            RotatePlayer(newAngles, TURN_AROUND_TIME);

            MovePlayer(reachedPos, time);
        };

        touchManager.StopHoldAction = () =>
        {
            StopAllActions();
        };
    }
}
