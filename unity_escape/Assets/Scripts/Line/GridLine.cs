using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLine : LineBase
{
    private new void Start()
    {
        base.Start();

        SetLineWidth(0.2f, 0.2f);
    }

    private void DrawGrid(Vector3 centerPosition, float width)
    {

    }
}
