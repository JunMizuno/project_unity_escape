using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCube : ForceObjectBase
{
    private void Start()
    {
        AddForce(new Vector3(100.0f, 400.0f, 0.0f));
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
