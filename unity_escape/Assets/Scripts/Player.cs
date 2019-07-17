using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : ObjectBase
{
    protected override void Start()
    {
        base.Start();

        var touchArea = GameManager.Instance.GetTouchManager();
        if (touchArea != null)
        {
            touchArea.TouchUpAction = (x) =>
            {
                var worldPosition = touchArea.GetCurrentTouchActionPositionInWorld();
                worldPosition.y = 0.5f;

                var sequence = DOTween.Sequence();
                sequence.Append(this.transform.DOMove(worldPosition, 1.0f)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        sequence.Kill();
                    })
                    );
                sequence.Play();
            };
        }
    }
}
