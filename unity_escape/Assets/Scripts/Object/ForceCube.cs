using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCube : ForceObjectBase
{
    private void Start()
    {
        AddForce(new Vector3(1.0f, 10.0f, 0.0f), ForceMode.VelocityChange);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    protected override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
    }
}
