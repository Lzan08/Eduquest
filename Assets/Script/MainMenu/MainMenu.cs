using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private PlayfabManager playfab; 
    //public static string stringNamePlayer;

    [Space]
    public GameObject panelWelcome;
    public TMP_Text textWelcome;
    public float delayLoad;

    void Start()
    {
       
        playfab = PlayfabManager.Instance;
    }

    public void ButtonLeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }

    public void Materi()
    {
        SceneManager.LoadScene("Materi");
    }

    public void Achievement()
    {
        SceneManager.LoadScene("Achievement");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GamePlay()
    {
        // if (playfab != null)
        {
            panelWelcome.SetActive(true);
            Invoke("CountinueLoadGame", delayLoad);
        }

    }

    void CountinueLoadGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
