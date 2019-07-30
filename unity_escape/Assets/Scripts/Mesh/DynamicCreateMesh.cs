using System.Collections;
using UnityEngine;

// @memo. RequireComponentで指定することでそのスクリプトが必須であることを示す
// @memo. アタッチし忘れ防止の役割を持つ(自動でアタッチされる)
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class DynamicCreateMesh : MonoBehaviour
{
    [SerializeField]
    private Material unlitColorMaterial;

    private float colorControlValue = 0.5f;

    private void Start()
    {
        CreateSimpleTriangleMesh();
    }

    private void Update()
    {
        SetColorControlValue();   
    }

    private void CreateSimpleTriangleMesh()
    {
        var mesh = new Mesh();

        // 頂点座標
        mesh.vertices = new Vector3[]
        {
            new Vector3(0.0f, 2.0f),
            new Vector3(1.0f, 0.0f),
            new Vector3(-1.0f, 0.0f),
        };

        // 頂点を結ぶ順番
        mesh.triangles = new int[]
        {
            0,
            1,
            2,
        };

        ChangeVertexColor(ref mesh);

        // 法線ベクトルの再計算処理
        mesh.RecalculateNormals();

        // MeshデータをMeshRendererに渡す役割
        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;

        ChangeMaterial();
    }

    private void ChangeVertexColor(ref Mesh mesh)
    {
        if (mesh == null)
        {
            return;
        }

        mesh.colors = new Color[]
        {
            Color.white,
            Color.red,
            Color.green,
        };
    }

    private void ChangeMaterial()
    {
        if (unlitColorMaterial == null)
        {
            return;
        }

        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = unlitColorMaterial;
    }

    private void SetColorControlValue()
    {
        colorControlValue += (Time.deltaTime / 4.0f);
        if (colorControlValue > 1.0f)
        {
            colorControlValue = 0.5f;
        }

        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.SetFloat("_ColorControlValue", colorControlValue);
    }
}
