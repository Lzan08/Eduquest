using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMenu : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoLeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}