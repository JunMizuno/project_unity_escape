using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : ObjectBase
{
    private bool isTurned;
    private IEnumerator rotateCoroutine;

    private IEnumerator moveCoroutine;

    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// 回転処理
    /// </summary>
    protected void RotatePlayer(Vector3 angles, float turnAroundTime)
    {
        if (isTurned)
        {
            return;
        }

        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }
        rotateCoroutine = null;

        rotateCoroutine = RotateToAngle(angles, turnAroundTime);
        StartCoroutine(rotateCoroutine);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="turnAroundTime"></param>
    /// <returns></returns>
    protected IEnumerator RotateToAngle(Vector3 angles, float turnAroundTime)
    {
        isTurned = true;

        float totalTime = 0.0f;
        Vector3 anglesDiff = angles - this.transform.localEulerAngles;

        while (totalTime < turnAroundTime)
        {
            totalTime += Time.deltaTime;

            float addAngleValueX = (anglesDiff.x / turnAroundTime) * Time.deltaTime;
            float addAngleValueY = (anglesDiff.y / turnAroundTime) * Time.deltaTime;
            float addAngleValueZ = (anglesDiff.z / turnAroundTime) * Time.deltaTime;

            Vector3 addAngle = new Vector3(addAngleValueX, addAngleValueY, addAngleValueZ);
            this.transform.eulerAngles = this.transform.eulerAngles + addAngle;

            yield return null;
        }

        isTurned = false;

        yield break;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="reachedPosition"></param>
    protected void MovePlayer(Vector3 reachedPosition, float time)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = null;

        moveCoroutine = MoveToPosition(reachedPosition, time);
        StartCoroutine(moveCoroutine);
    }

    protected IEnumerator MoveToPosition(Vector3 reachedPosition, float time)
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

    /// <summary>
    /// 全ての移動アクションを止める
    /// </summary>
    protected void StopAllActions()
    {
        StopRotateCoroutine();
        StopMoveCoroutine();
    }

    /// <summary>
    /// 回転処理を止める
    /// </summary>
    private void StopRotateCoroutine()
    {
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }
        rotateCoroutine = null;
    }

    /// <summary>
    /// 移動処理を止める
    /// </summary>
    private void StopMoveCoroutine()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = null;
    }
}
