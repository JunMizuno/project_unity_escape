using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ForceObject : MonoBehaviour
{
    private new Rigidbody rigidbody;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        //AddForce(new Vector3(-200.0f, 0.0f, 0.0f));
    }

    private void Update()
    {
        AddForce(new Vector3(-10.0f, 0.0f, 0.0f));
    }

    private void AddForce(Vector3 vec)
    {
        if (rigidbody == null)
        {
            return;
        }

        rigidbody.AddForce(vec);
    }

    /// <summary>
    /// @memo. 相手と当たる度に呼ばれる
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(string.Format("ForceObjectの接触判定開始時 当たった相手:{0}", collision.gameObject.name).WithColorTag(Color.yellow));
    }

    /// <summary>
    /// @memo. Exitは最初に当たった相手にのみしか呼ばれない
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(string.Format("ForceObjectの接触判定終了時 当たった相手:{0}", collision.gameObject.name).WithColorTag(Color.white));
    }
}
