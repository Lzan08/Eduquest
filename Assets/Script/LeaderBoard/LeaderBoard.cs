using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.SceneManagement;
using Ink.Parsed;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    [Header("UI LeaderBoard")]
    [SerializeField] private GameObject panelParentLeaderBoard;
    public TMP_Text textPageLeaderBoard;
    public Button buttonLeft, buttonRight;
    public int currentPage;
    public int maxPage;
    public int multipleIndex = 5; 
    public int indexLeaderboard;

    public static LeaderBoard Instance { get; private set; }

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        currentPage = 1;
        indexLeaderboard = 0;
        buttonLeft.interactable = false;
        buttonRight.interactable = true;
        GetLeaderBoard();
    }

    

    public void buttonLeftRightLeaderBoard()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Left")
        {
            currentPage--;
            indexLeaderboard -= multipleIndex;
        }
        else
        {
            currentPage++;
            indexLeaderboard += multipleIndex;
        }

        textPageLeaderBoard.text = currentPage.ToString();
        GetLeaderBoard();
    }

    public void GetLeaderBoard()
    {
        
        buttonLeft.interactable = false;
        buttonRight.interactable = false;

        var request = new GetLeaderboardRequest
        {
            StatisticName = "LeaderBoard",
            StartPosition = indexLeaderboard,
            MaxResultsCount = multipleIndex
        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardSuccess, OnLeaderBoardFailed);
    }

    public void RefreshLeaderBoard()
    {
        GetLeaderBoard();
    }

   private void OnLeaderBoardSuccess(GetLeaderboardResult result)
{
    if (panelParentLeaderBoard == null)
    {
        return;
    }

    
    for (int i = 0; i < panelParentLeaderBoard.transform.childCount; i++)
    {
        panelParentLeaderBoard.transform.GetChild(i).gameObject.SetActive(false);
    }

   
    if (currentPage > 1)
    {
        buttonLeft.interactable = true; 
    }
    else
    {
        buttonLeft.interactable = false; 
    }

    
    if (result.Leaderboard.Count < multipleIndex)
    {
        buttonRight.interactable = false; 
    }
    else
    {
        buttonRight.interactable = true; 
    }

    
    for (int i = 0; i < result.Leaderboard.Count; i++)
    {
        Transform entryTransform = panelParentLeaderBoard.transform.GetChild(i);
        entryTransform.gameObject.SetActive(true); 

        result.Leaderboard[i].Position = indexLeaderboard + i + 1;
        
        TMP_Text positionText = entryTransform.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text nameText = entryTransform.GetChild(1).GetComponent<TMP_Text>();
        TMP_Text scoreText = entryTransform.GetChild(2).GetComponent<TMP_Text>();

        positionText.text = result.Leaderboard[i].Position.ToString()+".";
        nameText.text = result.Leaderboard[i].DisplayName;
        scoreText.text = result.Leaderboard[i].StatValue.ToString();

        positionText.color = Color.black;
        nameText.color = Color.black;
        scoreText.color = Color.black;
        
        if(result.Leaderboard[i].PlayFabId==PlayfabManager.Instance.IdPlayer)
        {
            positionText.color = Color.blue;
            nameText.color = Color.blue;
            scoreText.color = Color.blue;
        }
    }

   
    int totalEntries = result.Leaderboard.Count;
    maxPage = Mathf.CeilToInt((float)totalEntries / multipleIndex);
    textPageLeaderBoard.text = currentPage.ToString(); 
}



    private void OnLeaderBoardFailed(PlayFabError error)
    {
       
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
