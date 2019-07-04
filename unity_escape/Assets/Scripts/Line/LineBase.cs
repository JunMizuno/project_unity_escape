using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LineBase : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> positionsList;
    private float startWidth;
    private float endWidth;

    protected void Start()
    {
        lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = this.startWidth;
        lineRenderer.endWidth = this.endWidth;
        lineRenderer.material = new Material(Shader.Find("Starndard")) { color = Color.black };
        positionsList = new List<Vector3>();
        positionsList.Add(new Vector3(0.0f, 0.0f, 0.0f));
        positionsList.Add(new Vector3(0.0f, 0.0f, 0.0f));
        lineRenderer.SetPositions(positionsList.ToArray());
    }

    protected void SetLinePositions(List<Vector3> vectorList)
    {
        positionsList = vectorList;
        lineRenderer.positionCount = positionsList.Count;
        lineRenderer.SetPositions(positionsList.ToArray());
    }
}
