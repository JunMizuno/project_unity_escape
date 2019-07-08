using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StandardLine : LineBase
{
    private new void Start()
    {
        base.Start();

        // @memo. 以下のように指定可能
        SetLineWidth(0.2f, 0.2f);

        List<Vector3> positionsList = new List<Vector3>();
        positionsList.Add(new Vector3(-5.0f, 0.0f, -5.0f));
        positionsList.Add(new Vector3(-5.0f, 0.0f, 5.0f));
        positionsList.Add(new Vector3(5.0f, 0.0f, 5.0f));
        positionsList.Add(new Vector3(5.0f, 0.0f, -5.0f));
        SetLinePositions(positionsList, true);

        Material material = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/SampleMaterial.mat");
        SetLineMaterial(material);
    }
}
