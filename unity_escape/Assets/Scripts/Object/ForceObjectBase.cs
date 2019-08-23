using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
abstract public class ForceObjectBase : MonoBehaviour
{
    private new Rigidbody rigidbody;

    /// <summary>
    /// インスタンス時
    /// </summary>
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 開始時
    /// </summary>
    private void Start()
    {

    }

    /// <summary>
    /// 更新時
    /// </summary>
    private void Update()
    {

    }

    /// <summary>
    /// 指定したベクトル分の衝撃を加える
    /// Fource:質量を反映して継続的に追加
    /// Acceleration:質量を無視して継続的に追加
    /// Impulse:質量を反映して瞬間的に追加
    /// VelocityChange:質量を無視して瞬間的に追加
    /// </summary>
    /// <param name="vec"></param>
    /// <param name="forceMode"></param>
    protected void AddForce(Vector3 vec, ForceMode forceMode = ForceMode.Force)
    {
        if (rigidbody == null)
        {
            return;
        }

        rigidbody.AddForce(vec, forceMode);
    }

    /// <summary>
    /// @memo. 相手と当たる度に呼ばれる
    /// </summary>
    /// <param name="collision"></param>
    virtual protected void OnCollisionEnter(Collision collision)
    {
        Debug.Log(string.Format("ForceObjectの接触判定開始時 当たった相手:{0}", collision.gameObject.name).WithColorTag(Color.yellow));
    }

    /// <summary>
    /// @memo. Exitは最初に当たった相手にのみしか呼ばれない
    /// </summary>
    /// <param name="collision"></param>
    virtual protected void OnCollisionExit(Collision collision)
    {
        Debug.Log(string.Format("ForceObjectの接触判定終了時 当たった相手:{0}", collision.gameObject.name).WithColorTag(Color.white));
    }
}
