using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AdvanceDialogueManager : MonoBehaviour
{
    private AdvanceDialogueSO currentConversation;
    private int stepNum;
    private bool dialogueActivated;

    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text actor;
    [SerializeField] private Image portrait;
    [SerializeField] private TMP_Text dialogueText;

    private string currentSpeaker;
    private Sprite currentPortrait;
    public ActorSO[] actorSO;

    
    [SerializeField] private GameObject[] optionButton;
    [SerializeField] private TMP_Text[] optionButtonText;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject joyStick;

   
    public bool dialogBoxActive; 

    private NPCDialogue currentNpcDialogue;

    void Start()
    {
        dialogueCanvas.SetActive(false);
        optionsPanel.SetActive(false);
        optionButtonText = new TMP_Text[optionButton.Length];

        for (int i = 0; i < optionButton.Length; i++)
        {
            optionButtonText[i] = optionButton[i].GetComponentInChildren<TMP_Text>();
        }

        for (int i = 0; i < optionButton.Length; i++)
            optionButton[i].SetActive(false);
    }

    void Update()
    {
        if (dialogueActivated && Input.GetMouseButtonDown(0))
        {
            if (dialogBoxActive) return;

            if (optionsPanel.activeSelf)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.CompareTag("OptionButton"))
                {
                    return; 
                }
            }
            else
            {
                if (stepNum >= currentConversation.actors.Length)
                {
                    TurnOffDialogue();
                }
                else
                {
                    PlayDialogue();
                }
            }
        }
    }

    void PlayDialogue()
    {
        dialogueCanvas.SetActive(true);
        joyStick.SetActive(false);
        if (currentConversation.actors[stepNum] == DialogueActors.Random)
        {
            SetActorInfo(false);
        }
        else
        {
            SetActorInfo(true);
        }

        actor.text = currentSpeaker;
        portrait.sprite = currentPortrait;

        if (currentConversation.actors[stepNum] == DialogueActors.Branch)
        {
            optionsPanel.SetActive(true);
            int length = Mathf.Min(currentConversation.optionText.Length, optionButton.Length, optionButtonText.Length);

            for (int i = 0; i < length; i++)
            {
                if (currentConversation.optionText[i] == null)
                {
                    optionButton[i].SetActive(false);
                }
                else
                {
                    optionButtonText[i].text = currentConversation.optionText[i];
                    optionButton[i].SetActive(true);
                }
            }
        }
        else
        {
            optionsPanel.SetActive(false);
        }

        if (stepNum < currentConversation.dialogue.Length)
        {
            dialogueText.text = currentConversation.dialogue[stepNum];
        }
        else
        {
            optionsPanel.SetActive(true);
        }

        stepNum += 1;
        Time.timeScale = 0f;
    }

    public void InitiateDialogue(NPCDialogue npcDialogue)
    {
        
        if (dialogBoxActive) return;

        currentNpcDialogue = npcDialogue;
        currentConversation = npcDialogue.conversation[0];
        dialogueActivated = true;
        stepNum = 0;
        SetActorInfo(true);
        PlayDialogue();
    }

    void SetActorInfo(bool recurringCharacter)
{
    if (recurringCharacter)
    {
        for (int i = 0; i < actorSO.Length; i++)
        {
            
            if (actorSO[i].actorName == currentConversation.actors[stepNum].ToString())
            {
                
                currentSpeaker = actorSO[i].actorDisplayName;
                currentPortrait = actorSO[i].actorPortrait;
                return;
            }
        }
    }
    else
    {
        currentSpeaker = currentConversation.randomActorName;
        currentPortrait = currentConversation.randomActorPortrait;
    }
}


    public void Option(int optionNum)
    {
        foreach (GameObject button in optionButton)
            button.SetActive(false);

        if (optionNum == 0)
            currentConversation = currentConversation.option0;
        if (optionNum == 1)
            currentConversation = currentConversation.option1;
        if (optionNum == 2)
            currentConversation = currentConversation.option2;
        if (optionNum == 3)
            currentConversation = currentConversation.option3;

        stepNum = 0;
        PlayDialogue();
    }

    public void TurnOffDialogue()
{
    stepNum = 0;
    dialogueActivated = false;
    optionsPanel.SetActive(false);
    dialogueCanvas.SetActive(false);

    if (currentNpcDialogue != null)
    {
        if (currentNpcDialogue.CompareTag("QuestNpc"))
        {
            currentNpcDialogue.PlayDialogBox();
        }
        else
        {
            joyStick.SetActive(true);
            Time.timeScale = 1f;
            currentNpcDialogue.DestroyAfterDialogue(); 
        }
    }
}

}

public enum DialogueActors
{
    Headmaster,
    Guru,
    Ari,
    Ibu,
    Ujang,
    Aldi,
    BuAyni,
    PakSobri,
    Random,
    Branch,
    stringNamePlayer
}


