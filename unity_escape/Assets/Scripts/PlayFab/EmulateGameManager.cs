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

    private int useLifeTimeStamp;
    public int UseLifeTimeStamp
    {
        get { return useLifeTimeStamp; }
        set { useLifeTimeStamp = value; }
    }

    private int currentLifeCount;
    public int CurrentLifeCount
    {
        get { return currentLifeCount; }
    }

    //private readonly int intervalTimeForRecoverLife = 300;
    private readonly int intervalTimeForRecoverLife = 5;
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
        }

        //isLogin = false;
        isLogin = true;
    }

    private void Start()
    {
        // @todo. ここでは前回ライフを使ったタイムスタンプを(追加設定して)取ること
        // @todo. ログインスタンプがゼロの場合はライフ5個分相当の数値を入れること
        // @todo. 対応が終わったらこれをログイン時に行うように変更
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
        // @todo. ここのロジックも変更が必要(loginTimeStampを前回使った時間に差し替える)
        currentLifeCount = (DateTime.Now.ToTimeStamp() - useLifeTimeStamp) / intervalTimeForRecoverLife;
        if (currentLifeCount > maxLifeCount)
        {
            currentLifeCount = maxLifeCount;
        }

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
