using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : ObjectBase
{
    private readonly float PLAYER_DEFAULT_HEIGHT = 0.0f;
    private readonly float MOVING_DISTANCE_PER_SECOND = 0.2f;
    private readonly float MOVING_MIN_TIME = 2.0f;
    private readonly float MOVING_MAX_TIME = 8.0f;
    private readonly float ADJUST_ANGLE_Y = -90.0f;
    private readonly float TURN_AROUND_TIME = 0.2f;

    private Sequence sequence;

    protected override void Start()
    {
        base.Start();

        var touchArea = GameManager.Instance.GetTouchManager();
        if (touchArea != null)
        {
            touchArea.TouchUpAction = (x) =>
            {
                sequence.Kill();

                // @todo. フィールドにマーカーを追加

                var touchWorldPosition = touchArea.GetCurrentTouchActionPositionInWorld();
                // @memo. プレイヤーの高さは変更しないため固定値を設定
                touchWorldPosition.y = PLAYER_DEFAULT_HEIGHT;

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

                // @memo. ワールドでは右向きゼロ度から反時計回りにプラスした計算で角度が出ている
                // @memo. プロジェクトではキャラがZ軸に対して正面を向いている状態がゼロ度となっているため調整
                float newAngleY = angle + ADJUST_ANGLE_Y;
                Vector3 newAngles = new Vector3(this.transform.localEulerAngles.x, newAngleY.ByAbs(), this.transform.localEulerAngles.z);

                sequence = DOTween.Sequence();
                sequence.Append(this.transform.DOMove(touchWorldPosition, time)
                    .SetEase(Ease.OutSine)
                    .OnComplete(() =>
                    {
                        sequence.Kill();

                        // @todo. フィールドのマーカーを削除
                    })
                    );
                sequence.Join(this.transform.DORotate(newAngles, TURN_AROUND_TIME)
                    .SetEase(Ease.InSine)
                    .OnComplete(() =>
                    {
                    })
                    );
                sequence.Play();
            };
        }
    }

    private void OnDisable()
    {
        sequence.Kill();
    }

    private void OnDestroy()
    {
        sequence.Kill();
    }
}
