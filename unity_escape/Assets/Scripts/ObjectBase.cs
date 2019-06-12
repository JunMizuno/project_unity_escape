using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    virtual protected void Start()
    {
        Debug.Log("------ObjectBase:Start()");
    }

    virtual protected void Update()
    {
        Debug.Log("------ObjectBase:Update()");
    }
}
