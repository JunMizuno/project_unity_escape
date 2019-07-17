using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelBase : ObjectBase
{
    // @memo. テスト用
    private int count;

    // Start is called before the first frame update
    new void Start()
    {
        //this.transform.DOMoveZ(10.0f, 2.0f);
        //DOTween.To(() => count, (n) => count = n, 50, 10.0f);
    }

    // Update is called once per frame
    new void Update()
    {
        //Debug.Log("<color=red>" + "count:" + count + "</color>");
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
