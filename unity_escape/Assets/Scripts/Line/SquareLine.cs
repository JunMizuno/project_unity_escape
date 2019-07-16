using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareLine : LineBase
{
    private new void Start()
    {
        base.Start();

        SetLineWidth(0.2f, 0.2f);
    }

    private void DrawSquare(Vector3 centerPosition, float width, float hight, Vector3 rotate)
    {

    }
}
