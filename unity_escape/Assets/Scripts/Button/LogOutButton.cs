using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOutButton : ButtonBase
{
    new public void OnTouchButtonAction()
    {
        Debug.Log("LogOutButton OnTouchButtonAction".WithColorTag(Color.yellow));
    }
}
