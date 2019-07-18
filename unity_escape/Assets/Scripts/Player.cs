using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : ObjectBase
{
    private readonly float PLAYER_DEFAULT_HEIGHT = 0.5f;
    private readonly float MOVING_DISTANCE_PER_SECOND = 1.0f;

    private Sequence sequence;

    protected override void Start()
    {
        base.Start();

        var touchArea = GameManager.Instance.GetTouchManager();
        if (touchArea != null)
        {
            touchArea.TouchUpAction = (x) =>
            {
                var touchWorldPosition = touchArea.GetCurrentTouchActionPositionInWorld();
                // @memo. プレイヤーの高さは変更しないため固定値を設定
                touchWorldPosition.y = PLAYER_DEFAULT_HEIGHT;

                sequence.Kill();
                sequence = DOTween.Sequence();
                sequence.Append(this.transform.DOMove(touchWorldPosition, 1.0f)
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
