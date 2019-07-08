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
        lineRenderer.material = new Material(Shader.Find("Standard")) { color = Color.black };
        positionsList = new List<Vector3>();
        positionsList.Add(new Vector3(0.0f, 0.0f, 0.0f));
        positionsList.Add(new Vector3(0.0f, 0.0f, 0.0f));
        lineRenderer.SetPositions(positionsList.ToArray());
    }

    protected void SetLineWidth(float startWidth, float endWidth)
    {
        this.startWidth = startWidth;
        this.endWidth = endWidth;
        lineRenderer.startWidth = this.startWidth;
        lineRenderer.endWidth = this.endWidth;
    }

    protected void SetLineMaterial(Material material)
    {
        lineRenderer.material = material;
    }

    protected void SetLineColor(Color startColor, Color endColor)
    {
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;
    }        

    protected void SetLinePositions(List<Vector3> vectorList, bool isLoop)
    {
        positionsList = vectorList;
        lineRenderer.positionCount = positionsList.Count;
        lineRenderer.SetPositions(positionsList.ToArray());
        lineRenderer.loop = isLoop;
    }
}
