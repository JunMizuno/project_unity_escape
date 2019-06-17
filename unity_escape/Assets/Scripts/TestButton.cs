using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public void OnClickToOpenOdoroboWithParam()
    {
        Application.OpenURL("ponos-odorobo://ponos/battlecats4");
    }

    public void OnClickToOpenOdorobo()
    {
        Application.OpenURL("ponos-odorobo://");
    }

    public void OnClickToOpenBattleCatsWithParam()
    {
        Application.OpenURL("ponos-battlecats4://ponos/odorobo1");
    }

    public void OnClickToOpenBattleCats()
    {
        Application.OpenURL("ponos-battlecats4://");
    }
}
