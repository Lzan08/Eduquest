using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TMP_Text scoreText;
   
    public int increaseScore;
    public int decreaseScore;
    

    private void Start()
    {
        
        string currentLevel = SceneManager.GetActiveScene().name;

       
        ResetScore();

        
        score = PlayerPrefs.GetInt(currentLevel + "_Score", 0);
        UpdateScoreText();
    }

    public void IncreaseScore()
    {
        score+= increaseScore;
        SaveScore();
        UpdateScoreText();
    }

    public void DecreaseScore()
    {
        score = Mathf.Max(0, score - decreaseScore);
        SaveScore();
        UpdateScoreText();
    }

    private void SaveScore()
    {
       
        string currentLevel = SceneManager.GetActiveScene().name;

      
        PlayerPrefs.SetInt(currentLevel + "_Score", score);
        PlayerPrefs.Save(); 
    }

        private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    
    private void ResetScore()
    {
        score = 0;
        SaveScore();
        UpdateScoreText();
    }

    //    public int GetTotalScore()
    // {
    //     int totalScore = 0;

     
    //     for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
    //     {
    //         string levelName = SceneUtility.GetScenePathByBuildIndex(i);
    //         levelName = System.IO.Path.GetFileNameWithoutExtension(levelName);

           
    //         int scoreInLevel = PlayerPrefs.GetInt(levelName + "_Score", 0);

           
    //         totalScore += scoreInLevel;
    //     }

    //     return totalScore;
    // }     
}