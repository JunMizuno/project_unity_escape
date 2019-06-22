using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmulateGameManager : MonoBehaviour
{
    private static EmulateGameManager instance;
    public static EmulateGameManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

    private int loginTimeStamp;
    public int LoginTimeStamp
    {
        get { return loginTimeStamp; }
        set { loginTimeStamp = value; }
    }

    private int currentLifeCount;
    public int CurrentLifeCount
    {
        get { return currentLifeCount; }
    }

    private readonly int intervalTimeForRecoverLife = 300;
    private readonly int maxLifeCount = 5;

    private bool isLogin;
    public bool IsLogin
    {
        set { isLogin = value; }
    }

    [SerializeField]
    public LifeImage[] lifeImageArray = new LifeImage[5];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PlayerPrefs.SetString(UtilitySettings.UNIQUE_IDENTIFIER_INDEX, SystemInfo.deviceUniqueIdentifier);
        }

        isLogin = false;
    }

    private void Start()
    {
        // @todo. ここでは前回ライフを使ったタイムスタンプを(追加設定して)取ること
        // @todo. ログインスタンプがゼロの場合はライフ5個分相当の数値を入れること
        loginTimeStamp = PlayerPrefs.GetInt(UtilitySettings.RECORD_LOGIN_TIME_KEY, 0);
        if (loginTimeStamp == 0)
        {

        }
    }

    private void Update()
    {
        if (isLogin)
        {
            CalcCurrentLifeCount();
        }
    }

    private void CalcCurrentLifeCount()
    {
        currentLifeCount = (DateTime.Now.ToTimeStamp() - loginTimeStamp) / intervalTimeForRecoverLife;

        for (int i = 0; i <= (maxLifeCount - 1); i++)
        {
            if (!lifeImageArray[i])
            {
                continue;
            }

            if (i <= (currentLifeCount - 1))
            {
                if (lifeImageArray[i].IsShow())
                {
                    continue;
                }

                lifeImageArray[i].ScaleUp(1.0f, 1.0f);
            }
            else
            {
                if (!lifeImageArray[i].IsShow())
                {
                    continue;
                }

                lifeImageArray[i].HideLifeImage();
            }
        }
    }
}
