using UnityEngine;
using System.Collections;
public class ColliderPlayer : MonoBehaviour
{
    AudioManager audioManager;
    private int BGMindex;
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private MissionPanelManager missionPanelManager; 
    [SerializeField] private GameObject DialogBox;
    [SerializeField] private GameObject Joystick;
    [SerializeField] private SoalManager soalManager;
    [SerializeField] private GameObject PaperButton;
    [SerializeField] private GameObject PaperPanel;

    private Rigidbody2D rb;
    private GameObject currentEnemy; 
    private GameObject currentNPC;   

    void Start()
    {
        DialogBox.SetActive(false);
        rb = GetComponent<Rigidbody2D>();

        if (soalManager != null)
        {
            soalManager.OnQuestionsComplete += HandleQuestionsComplete;
        }
        else
        {
        }
    }

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        missionPanelManager.StartMissionByGiver(other.gameObject);

        if (other.gameObject.CompareTag("Paper"))
        {
            HandlePaperTrigger(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Quest"))
        {
            HandleQuestCollision(other.gameObject);
        }
    }

    void HandlePaperTrigger(GameObject paper)
    {
        PaperButton.SetActive(true);
        Destroy(paper);
        PaperPanel.SetActive(true);
        Time.timeScale = 0f;
        Joystick.SetActive(false);
    }

  

    void HandleQuestCollision(GameObject questObject)
    {
        audioManager.PlaySFX(audioManager.tabrak);
        audioManager.ChangeBGM(2);
        currentEnemy = questObject;

        if (Joystick != null) Joystick.SetActive(false);
        if (DialogBox != null) DialogBox.SetActive(true);

        if (soalManager != null)
        {
            soalManager.SetEnemyQuestions(questObject);
            Time.timeScale = 0f; 
        }
        
    }

    void HandleQuestNPCTrigger(GameObject npc)
    {
        audioManager.PlaySFX(audioManager.tabrak);
        audioManager.ChangeBGM(2);
        currentNPC = npc;

      
        if (DialoguePanel != null) DialoguePanel.SetActive(true);

      
        if (DialoguePanel != null) DialoguePanel.SetActive(false);
        if (DialogBox != null) DialogBox.SetActive(true);

        if (soalManager != null)
        {
            soalManager.SetEnemyQuestions(npc);
            Time.timeScale = 0f; 
        }
    }
    void HandleQuestionsComplete()
    {
        audioManager.ChangeBGM(1);
        if (Joystick != null) Joystick.SetActive(true); 
        if (DialogBox != null) DialogBox.SetActive(false);
        Time.timeScale = 1f; 

        if (currentEnemy != null)
        {
            Destroy(currentEnemy);
            currentEnemy = null;
        }

        if (currentNPC != null)
        {
            Destroy(currentNPC);
            currentNPC = null;
        }
    }

    void OnDestroy()
    {
        if (soalManager != null)
        {
            soalManager.OnQuestionsComplete -= HandleQuestionsComplete;
        }
    }
}



