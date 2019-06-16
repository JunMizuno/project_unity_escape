using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public void OnClickToOpenOdoroboWithParam()
    {
        Application.OpenURL("ponos-odorobo://ponos-battlecats");
    }

    public void OnClickToOpenOdorobo()
    {
        Application.OpenURL("ponos-odorobo://");
    }

    public void OnClickToOpenBattleCats()
    {
        Application.OpenURL("ponos-battlecats4://");
    }
}
