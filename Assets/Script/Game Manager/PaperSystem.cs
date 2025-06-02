using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PaperSystem : MonoBehaviour
{
    [SerializeField] GameObject PaperPanel;
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject dialoguePanel;

    [SerializeField] GameObject Joystick;
    [SerializeField] GameObject Pause;

    public Image imageInformasi;
    public Sprite[] spriteInformasi;
    public Button[] buttonControl;
    
    int pageMateri = 0; 

    public void buttonpanelPaper(){
        if(PaperPanel.activeInHierarchy == false){
            PaperPanel.SetActive(true);
            Joystick.SetActive(false);
            Time.timeScale =0f;
        }else{
            PaperPanel.SetActive(false);
            CheckPanels();
        }
        
    }

    void CheckPanels()
{
    

    
    if (dialogBox.activeSelf || dialoguePanel.activeSelf)
    {
        
        Joystick.SetActive(false);
        Time.timeScale = 0f; 
    }
    else
    {
        Joystick.SetActive(true);
        Time.timeScale = 1f; 
    }
}
    

    void Start()
    {
        
        buttonControl[0].interactable = false; 
        buttonControl[1].interactable = spriteInformasi.Length > 1; 
        imageInformasi.sprite = spriteInformasi[pageMateri]; 

    }

    public void buttonControlMateri()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Right")
        {
            pageMateri++;
            imageInformasi.sprite = spriteInformasi[pageMateri];

            
            buttonControl[0].interactable = pageMateri > 0;
            buttonControl[1].interactable = pageMateri < spriteInformasi.Length - 1;
        }
        else 
        {
            pageMateri--; 
            imageInformasi.sprite = spriteInformasi[pageMateri];

            
            buttonControl[0].interactable = pageMateri > 0; 
            buttonControl[1].interactable = pageMateri < spriteInformasi.Length - 1; 
        }
        Joystick.SetActive(false);
    }
}
