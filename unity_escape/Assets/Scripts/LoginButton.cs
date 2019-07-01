using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginButton : ButtonBase
{
    new public void OnTouchButtonAction()
    {
        // @todo. ここをPlayFab対応させる

        int currentTimeStamp = DateTime.Now.ToTimeStamp();
        EmulateGameManager.Instance.LoginTimeStamp = currentTimeStamp;
        EmulateGameManager.Instance.IsLogin = true;

        PlayerPrefs.SetInt(UtilitySettings.RECORD_LOGIN_TIME_KEY, EmulateGameManager.Instance.LoginTimeStamp);
    }
}
