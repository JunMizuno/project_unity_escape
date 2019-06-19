using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeComponent : MonoBehaviour
{
    public GameObject[] vertices = new GameObject[20];
    LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Unlit/Color"));
        line.positionCount = vertices.Length;

        foreach (GameObject v in vertices)
        {
            v.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void Update()
    {
        int idx = 0;
        foreach (GameObject v in vertices)
        {
            var trans = v.transform;
            if (trans.position.y >= 2.5f)
            {
                trans.position = new Vector3(trans.position.x, 2.5f, trans.position.z);
            }
            else if (trans.position.y <= 0.5f)
            {
                trans.position = new Vector3(trans.position.x, 0.5f, trans.position.z);
            }
            line.SetPosition(idx, trans.position);
            idx++;
        }
    }
}
