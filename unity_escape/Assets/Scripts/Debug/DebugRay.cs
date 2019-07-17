using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRay : MonoBehaviour
{
    private void Update()
    {
        DrawRay();
    }

    private void DrawRay()
    {
        Ray touchPointToRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(touchPointToRay.origin, touchPointToRay.direction * 1000.0f);
    }
}
