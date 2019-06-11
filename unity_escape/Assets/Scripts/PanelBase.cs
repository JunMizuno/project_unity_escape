using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.DOMoveZ(10.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
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
