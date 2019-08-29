using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private bool isInSight;
    public bool IsInSight
    {
        get
        {
            return isInSight;
        }
    }

    private void Start()
    {
        SetTouchAnction();
    }

    public void OnPointerEnter()
    {
        isInSight = true;
    }

    public void OnPointerExit()
    {
        isInSight = false;
    }

    public void OnPointerDown()
    {
        Debug.Log("OnPointerDown".WithColorTag(Color.white));
    }

    public void OnPointerMove()
    {
        Debug.Log("OnPointerMove".WithColorTag(Color.white));
    }

    public void OnPointerUp()
    {
        Debug.Log("OnPointerUp".WithColorTag(Color.white));
    }

    private void SetTouchAnction()
    {
        var touchManager = GameManager.Instance.GetTouchManager();
        if (touchManager == null)
        {
            return;
        }





    }
}
