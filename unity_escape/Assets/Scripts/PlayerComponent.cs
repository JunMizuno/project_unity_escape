using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    [SerializeField] CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Muteki(0.8f));
    }

    // ジャンプ中は衝突判定を無効化します。
    public void Jump()
    {
        StartCoroutine(Muteki(0.5f));
    }

    // プレイヤーに何かが当たったら Console に HIT!! と表示します。
    void OnCollisionEnter(Collision _)
    {
        // 時間があればゲームの終了処理に書き換えてみましょう。
        Debug.Log($"Hit!!");
    }

    // プレイ開始から0.5秒はプレイヤーの Collider を有効化せずに無敵扱いにします。
    IEnumerator Muteki(float waitSeconds)
    {
        // プレイヤーの 衝突判定を無効化します。
        capsuleCollider.enabled = false;

        // 0.5秒待ちます
        yield return new WaitForSeconds(waitSeconds);

        // プレイヤーの Collider を有効化します。
        capsuleCollider.enabled = true;
    }
}
