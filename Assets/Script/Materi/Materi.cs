using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Materi : MonoBehaviour
{
    public Image imageInformasi;
    public Sprite[] spriteInformasi;
    public Button[] buttonControl;
    int pageMateri = 0; 

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
    }

     public void BackButton()
    {
        
        SceneManager.LoadScene("MainMenu");
    }
}
