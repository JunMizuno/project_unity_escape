using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] PlayerComponent player;

    // ジャンプ中か判定するフラグ
    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 画面がタップされた & ジャンプ中ではない場合
        if (Input.GetMouseButtonDown(0) && !isJumping)
        {
            // ジャンプします
            StartCoroutine(Jump());
        }
    }

    // ジャンプの処理
    IEnumerator Jump()
    {
        // ジャンプ中のフラグを有効にします
        this.isJumping = true;

        // JumpTrigger を トリガーします
        playerAnimator.SetTrigger("JumpTrigger");

        // PlayerComponent側のジャンプ処理を呼び出します。
        player.Jump();

        // 0.5秒待ちます
        yield return new WaitForSeconds(0.5f);

        // ジャンプ中のフラグを無効にします
        this.isJumping = false;
    }
}
