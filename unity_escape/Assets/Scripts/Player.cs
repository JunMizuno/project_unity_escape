using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : ObjectBase
{
    // 数値を増やすとスピードダウン
    private readonly float MOVING_DISTANCE_PER_SECOND = 0.1f;
    private readonly float MOVING_MIN_TIME = 2.0f;
    private readonly float MOVING_MAX_TIME = 8.0f;
    private readonly float ADJUST_ANGLE_Y = -90.0f;
    private readonly float TURN_AROUND_TIME = 0.1f;

    private Sequence sequence;

    private bool isTurned;
    private IEnumerator rotateCoroutine;

    private IEnumerator moveCoroutine;

    protected override void Start()
    {
        base.Start();

        SetTouchedAction();
    }

    private void OnDisable()
    {
        sequence.Kill();
    }

    private void OnDestroy()
    {
        sequence.Kill();
    }

    public void SetTouchedAction()
    {
        var touchArea = GameManager.Instance.GetTouchManager();
        if (touchArea == null)
        {
            return;
        }

        // @todo. カメラモードによって切り替えられるようにする必要あり
        if (false)
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
            sequence.Kill();

            // @todo. フィールドにマーカーを追加

            var touchWorldPosition = touchManager.GetCurrentTouchActionPositionInWorld();

            // @memo. プレイヤーの高さは変更しないため固定値を設定
            touchWorldPosition.y = this.gameObject.transform.position.y;

            float distance = (touchWorldPosition - this.transform.position).sqrMagnitude;
            float time = Mathf.Clamp((distance * MOVING_DISTANCE_PER_SECOND), MOVING_MIN_TIME, MOVING_MAX_TIME);




            float touchWorldX = touchWorldPosition.x;
            float playerX = this.transform.position.x;
            float touchWorldZ = touchWorldPosition.z;
            float playerZ = this.transform.position.z;
            float dx = touchWorldX - playerX;
            float dz = touchWorldZ - playerZ;

            float rad = Mathf.Atan2(dz, dx);
            float angle = rad * Mathf.Rad2Deg;

            // @memo. プロジェクトではキャラがZ軸に対して正面を向いている状態がゼロ度となっているため調整
            float newAngleY = (angle + ADJUST_ANGLE_Y) * -1.0f;
            Vector3 newAngles = new Vector3(this.transform.localEulerAngles.x, newAngleY, this.transform.localEulerAngles.z);

            sequence = DOTween.Sequence();
            sequence.Append(this.transform.DORotate(newAngles, TURN_AROUND_TIME)
                .SetEase(Ease.InSine)
                .OnComplete(() =>
                {
                    StopMovingAction();
                })
            );
            sequence.Play();

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
            sequence.Kill();

            // @todo. 移動処理
            var currentPos = this.transform.position;
            var reachedPos = new Vector3(currentPos.x, currentPos.y, currentPos.z + 2.0f);

            float distance = (reachedPos - currentPos).sqrMagnitude;
            float time = Mathf.Clamp((distance * MOVING_DISTANCE_PER_SECOND), MOVING_MIN_TIME, MOVING_MAX_TIME);

            if (!isTurned)
            {
                RotatePlayer(TURN_AROUND_TIME);
            }

            MovePlayer(reachedPos, time);
        };

        touchManager.StopHoldAction = () =>
        {
            if (rotateCoroutine != null)
            {
                StopCoroutine(rotateCoroutine);
            }
            rotateCoroutine = null;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = null;
        };
    }

    /// <summary>
    /// 回転処理
    /// </summary>
    private void RotatePlayer(float turnAroundTime)
    {
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }
        rotateCoroutine = null;

        rotateCoroutine = RotateToAngle(turnAroundTime);
        StartCoroutine(rotateCoroutine);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="turnAroundTime"></param>
    /// <returns></returns>
    private IEnumerator RotateToAngle(float turnAroundTime)
    {







        isTurned = false;

        yield break;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="reachedPosition"></param>
    private void MovePlayer(Vector3 reachedPosition, float time)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = null;

        moveCoroutine = MoveToPosition(reachedPosition, time);
        StartCoroutine(moveCoroutine);
    }

    private IEnumerator MoveToPosition(Vector3 reachedPosition, float time)
    {
        float totalTime = 0.0f;
        Vector3 distance = reachedPosition - this.transform.position;

        // 目的座標に到達するまで
        while(totalTime < time)
        {
            totalTime += Time.deltaTime;

            float addValueX = (distance.x / time) * Time.deltaTime;
            float addValueY = (distance.y / time) * Time.deltaTime;
            float addValueZ = (distance.z / time) * Time.deltaTime;

            var addPos = new Vector3(addValueX, addValueY, addValueZ);
            this.transform.position = this.transform.position + addPos;

            yield return null;
        }

        yield break;
    }

    public void StopMovingAction()
    {
        sequence.Kill();
    }
}
