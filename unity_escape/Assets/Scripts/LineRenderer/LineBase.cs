using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBase : MonoBehaviour
{
    [SerializeField]
    public LineRenderer lineRenderer_;

    private void Start()
    {
        if (lineRenderer_ == null)
        {
            return;
        }

        //this.gameObject.AddComponent<LineRenderer>();

        // @memo. 太さの設定がキモの模様
        lineRenderer_.startWidth = 0.05f;
        lineRenderer_.endWidth = 0.05f;

        lineRenderer_.material = new Material(Shader.Find("Standard"))
        {
            color = Color.black
        };
        lineRenderer_.positionCount = 5;
        Vector3[] positions = new Vector3[] { new Vector3(1.0f, 1.0f, 1.0f), new Vector3(-1.0f, 1.0f, 1.0f), new Vector3(-1.0f, 1.0f, -1.0f), new Vector3(1.0f, 1.0f, -1.0f), new Vector3(1.0f, 1.0f, 1.0f) };
        lineRenderer_.SetPositions(positions);
    }

    private void Update()
    {

    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void OnDestroy()
    {

    }
}
