using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class UseHeartButton : ButtonBase
{
    new public void OnTouchButtonAction()
    {
        EmulateGameManager.Instance.UseLifeTimeStamp = DateTime.Now.ToTimeStamp();

        PlayFabManager.Instance.PlayFabGetTime();
    }
}
