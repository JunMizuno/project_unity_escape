using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : ObjectBase
{
    private readonly float PLAYER_DEFAULT_HEIGHT = 0.5f;
    private readonly float MOVING_DISTANCE_PER_SECOND = 0.2f;
    private readonly float MOVING_MIN_TIME = 2.0f;
    private readonly float MOVING_MAX_TIME = 8.0f;

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

                var touchWorldPosition = touchArea.GetCurrentTouchActionPositionInWorld();
                // @memo. プレイヤーの高さは変更しないため固定値を設定
                touchWorldPosition.y = PLAYER_DEFAULT_HEIGHT;

                float distance = (touchWorldPosition - this.transform.position).sqrMagnitude;
                float time = Mathf.Clamp((distance * MOVING_DISTANCE_PER_SECOND), MOVING_MIN_TIME, MOVING_MAX_TIME);

                sequence = DOTween.Sequence();
                sequence.Append(this.transform.DOMove(touchWorldPosition, time)
                    .SetEase(Ease.OutSine)
                    .OnComplete(() =>
                    {
                        sequence.Kill();
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
