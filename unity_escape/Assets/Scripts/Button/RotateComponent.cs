using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateComponent : MonoBehaviour
{
    float radius = 1.4f;
    float speed = 5f;
    float x;
    float angle = 0f;

    void Start()
    {
        x = transform.position.x;
    }

    void Update()
    {
        angle += speed;

        float y = 1.2f * radius * Mathf.Sin(-angle * Mathf.Deg2Rad) + 1f;
        float z = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        transform.position = new Vector3(x, y, z);
    }
}
