using System.Collections;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public AdvanceDialogueSO[] conversation;

    private Transform player;
    private SpriteRenderer BubbleSpeech;
    [SerializeField] private GameObject dialogBox; 

    private AdvanceDialogueManager advanceDialogueManager;

    [SerializeField] private GameObject joyStick;
    [SerializeField] private SoalManager soalManager;
    [SerializeField] private GameObject PaperButton;
    [SerializeField] private GameObject PaperPanel;
    [SerializeField] private bool destroyObject; 

    private GameObject parent;

    private bool dialogInitiated;

    void Start()
    {
        advanceDialogueManager = GameObject.Find("DialogueManager").GetComponent<AdvanceDialogueManager>();
        BubbleSpeech = GetComponent<SpriteRenderer>();
        BubbleSpeech.enabled = false;
        parent = transform.parent.gameObject;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !dialogInitiated)
        {
          
            BubbleSpeech.enabled = true;

            
            player = other.GetComponent<Transform>();

            
            if (player.position.x > transform.position.x && transform.parent.localScale.x < 0)
            {
                Flip();
            }
            else if (player.position.x < transform.position.x && transform.parent.localScale.x > 0)
            {
                Flip();
            }

           
            advanceDialogueManager.InitiateDialogue(this);
            dialogInitiated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BubbleSpeech.enabled = false;
            dialogInitiated = false;
        }
    }

    private void Flip()
    {
        Vector2 currentScale = transform.parent.localScale;
        currentScale.x *= -1;
        transform.parent.localScale = currentScale;
    }

    
    public void PlayDialogBox()
    {
        if (gameObject.CompareTag("QuestNpc"))
        {
            dialogBox.SetActive(true);
            if (joyStick != null) joyStick.SetActive(false);

            if (soalManager != null)
            {
                soalManager.SetEnemyQuestions(gameObject);
            }

            Time.timeScale = 0f; 

            if (destroyObject)
            {
                DestroyParent(); 
            }
        }
        else
        {
            
            dialogBox.SetActive(false);
            if (joyStick != null) joyStick.SetActive(true);
        }
    }

    private void DestroyParent()
    {
       
        if (parent != null)
        {
            Destroy(parent);
        }
    }

   
    public void DestroyAfterDialogue()
    {
        if (destroyObject)
        {
            DestroyParent(); 
        }
    }
}
