using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
    private static PlayFabManager instance;
    public static PlayFabManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        PlayFabLogin();
    }

    public void PlayFabLogin()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "132B0";
        }

        string uniqueIndex = PlayerPrefs.GetString(UtilitySettings.UNIQUE_IDENTIFIER_INDEX, "None");
        if (uniqueIndex == "None")
        {
            uniqueIndex = SystemInfo.deviceUniqueIdentifier;
            PlayerPrefs.SetString(UtilitySettings.UNIQUE_IDENTIFIER_INDEX, uniqueIndex);
        }
        Debug.Log("<color=cyan>" + "PlayFabId:" + uniqueIndex + "</color>");
        var request = new LoginWithCustomIDRequest { CustomId = uniqueIndex, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccessedCallback, OnLoginFailedCallback);
    }

    private void OnLoginSuccessedCallback(LoginResult result)
    {
        Debug.Log("PlayFab successful API call!");
        if (result.NewlyCreated)
        {
            PlayerPrefs.SetString(UtilitySettings.PLAYFAB_PLAYER_UNIQUE_INDEX_KEY, result.PlayFabId);
        }
        Debug.Log("<color=yellow>" + "PlayFabId:" + result.PlayFabId + "</color>");
        Debug.Log("<color=yellow>" + "PlayFabId:" + result.LastLoginTime + "</color>");

        PlayFabGetPlayerPlofile();
    }

    private void OnLoginFailedCallback(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    public void PlayFabGetPlayerPlofile()
    {
        string playFabId = PlayerPrefs.GetString(UtilitySettings.PLAYFAB_PLAYER_UNIQUE_INDEX_KEY, "");
        if (playFabId == String.Empty)
        {
            return;
        }

        // @memo. オプションでフラグを立てないと有効化しない
        PlayerProfileViewConstraints option = new PlayerProfileViewConstraints();
        option.ShowCreated = true;
        option.ShowLastLogin = true;
        var request = new GetPlayerProfileRequest { PlayFabId = playFabId, ProfileConstraints = option };
        PlayFabClientAPI.GetPlayerProfile(request, OnGetPlayerProfileSuccess, OnGetPlayerProfileFailed);
    }

    private void OnGetPlayerProfileSuccess(GetPlayerProfileResult result)
    {
        var playerProfile = result.PlayerProfile;
        Debug.Log("<color=magenta>" + "LastLogin:" + playerProfile.LastLogin + "</color>");

        int timeStamp = UtilitySystems.ConvertDateTimeToTimeStamp(playerProfile.LastLogin.Value);
        Debug.Log("<color=white>" + "LastLoginToTimeStamp:" + timeStamp + "</color>");
    }

    private void OnGetPlayerProfileFailed(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    public void PlayFabGetTime()
    {
        var request = new GetTimeRequest();
        PlayFabClientAPI.GetTime(request, OnGetTimeSuccess, OnGetTimeFailed);
    }

    private void OnGetTimeSuccess(GetTimeResult result)
    {
        DateTime currentDateTime = result.Time;
        Debug.Log("currentDateTime:" + currentDateTime);

        // @todo. それを使って処理をする
    }

    private void OnGetTimeFailed(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }
}
