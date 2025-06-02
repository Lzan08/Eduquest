using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TotalScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text totalScoreText;
    public int[] riwayatAchievement;

    private void Start()
    {
        DisplayTotalScore();
       // SendTotalScoreToPlayFab();
        LoadRiwayatAchievement();
        EndGame();
    }

    public int GetTotalScore()
    {
        int totalScore = 0;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string levelName = SceneUtility.GetScenePathByBuildIndex(i);
            levelName = System.IO.Path.GetFileNameWithoutExtension(levelName);
            int scoreInLevel = PlayerPrefs.GetInt(levelName + "_Score", 0);
            totalScore += scoreInLevel;
        }
        return totalScore;
    }

    public void DisplayTotalScore()
    {
        int totalScore = GetTotalScore();
        if (totalScoreText != null)
        {
            totalScoreText.text = "Total Score kamu adalah: " + totalScore.ToString();
        }
        else
        {
        }
    }
    public void EndGame()
    {
        int totalScore = GetTotalScore();
        PlayfabManager.Instance.sendLeaderBoard(totalScore);

        if (totalScore >= 100 && riwayatAchievement[0] == 0)
        {
            UnlockAchievement(0);
        }
        if (totalScore >= 300 && riwayatAchievement[1] == 0)
        {
            UnlockAchievement(1);
        }
        if (totalScore >= 500 && riwayatAchievement[2] == 0)
        {
            UnlockAchievement(2);
        }
    }

    private void LoadRiwayatAchievement()
    {
        for (int i = 0; i < riwayatAchievement.Length; i++)
        {
            riwayatAchievement[i] = PlayerPrefs.GetInt($"achievement{i}", 0);
        }
    }

    private void UnlockAchievement(int index)
    {
        if (riwayatAchievement[index] == 0)
        {
            riwayatAchievement[index] = 1;
            PlayerPrefs.SetInt($"achievement{index}", 1);
            PlayerPrefs.Save();
        }
    }
}
