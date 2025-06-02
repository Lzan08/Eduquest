using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager Instance { get; private set; }
    public static string stringNamePlayer;
    private string idPlayer;

    public string IdPlayer => idPlayer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        CheckInternetAndLogin();
    }

    void CheckInternetAndLogin()
    {
        if (IsInternetAvailable())
        {
            PlayfabUIManager.Instance.HideNoInternetPanel();
            LoginPlayFab();
        }
        else
        {
            PlayfabUIManager.Instance.ShowNoInternetPanel();
        }
    }

    bool IsInternetAvailable() =>
        Application.internetReachability != NetworkReachability.NotReachable;

    public void RetryInternetConnection() => CheckInternetAndLogin();

    void LoginPlayFab()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams { GetPlayerProfile = true }
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailed);
    }

    void OnLoginSuccess(LoginResult result)
    {
        idPlayer = result.PlayFabId;
        string name = result.InfoResultPayload?.PlayerProfile?.DisplayName;

        if (string.IsNullOrEmpty(name))
            PlayfabUIManager.Instance.ShowInputNamePanel();
        else
        {
            stringNamePlayer = name;
            PlayfabUIManager.Instance.HideInputNamePanel();
        }
    }

public void sendLeaderBoard(int score){
        var request = new UpdatePlayerStatisticsRequest{
            Statistics= new List<StatisticUpdate>{
                new StatisticUpdate{
                    StatisticName = "LeaderBoard",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, onUpdateLeaderBoardBerhasil, onUpdateLeaderBoardGagal);
    }

    void onUpdateLeaderBoardBerhasil(UpdatePlayerStatisticsResult result){
    }

    void onUpdateLeaderBoardGagal(PlayFabError err){
        
    }
    void OnLoginFailed(PlayFabError error)
    {
        Debug.LogError("Login gagal: " + error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate { StatisticName = "LeaderBoard", Value = score }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, result => { }, error =>
        {
            Debug.LogError("Leaderboard update failed: " + error.GenerateErrorReport());
        });
    }

    public void UpdateDisplayName(string newName, Action onSuccess = null, Action onFailure = null)
    {
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = newName };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request,
        result =>
        {
            stringNamePlayer = newName;
            onSuccess?.Invoke();
        },
        error =>
        {
            Debug.LogWarning("Failed to update display name.");
            onFailure?.Invoke();
        });
    }
}
