using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuAchievement : MonoBehaviour
{
    public GameObject panelAchievement;
    public GameObject detailAchievement;

    [Header("Achievement")]
    public Button[] buttonAchievement;
    public Sprite[] spriteAchievement;
    public Image imageDetailAchievement;
    public TMP_Text textDetailAchievement;
    public TMP_Text textDetailNamaAchievement;
    [TextArea(5, 5)]
    public string[] stringKeteranganAchievement;
    private int[] riwayatAchievement;

    void Start()
    {
        LoadAchievement();
    }

    public void ToggleDetailAchievement()
    {
        detailAchievement.SetActive(!detailAchievement.activeInHierarchy);
    }

    public void ToggleAchievementPanel()
    {
        panelAchievement.SetActive(!panelAchievement.activeInHierarchy);
    }

    public void UpdateDetailAchievement(int indexAchievement)
    {
        if (indexAchievement >= 0 && indexAchievement < riwayatAchievement.Length && riwayatAchievement[indexAchievement] == 1)
        {
            imageDetailAchievement.sprite = spriteAchievement[indexAchievement];
            textDetailNamaAchievement.text = spriteAchievement[indexAchievement].name;
            textDetailAchievement.text = stringKeteranganAchievement[indexAchievement];
            detailAchievement.SetActive(true);
        }
        else
        {
            detailAchievement.SetActive(false);
        }
    }

    void LoadAchievement()
    {
        int achievementCount = buttonAchievement.Length;
        riwayatAchievement = new int[achievementCount];

        for (int i = 0; i < achievementCount; i++)
        {
            riwayatAchievement[i] = PlayerPrefs.GetInt($"achievement{i}", 0);
            
            
if (riwayatAchievement[i] == 1)
{
    buttonAchievement[i].interactable = true;
    
   
    Transform achievementImageTransform = buttonAchievement[i].transform.Find("AchievementImage");
    if (achievementImageTransform != null)
    {
        Image achievementImage = achievementImageTransform.GetComponent<Image>();
        if (achievementImage != null)
        {
            achievementImage.sprite = spriteAchievement[i];
        }
    }
}
            else
            {
                buttonAchievement[i].interactable = false; 
            }
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
