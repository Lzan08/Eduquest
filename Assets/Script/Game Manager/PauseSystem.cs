using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class PauseSystem : MonoBehaviour
{
    [SerializeField] GameObject PanelMenu;
    [SerializeField] GameObject SettingMenu;
    [SerializeField] GameObject Dialogbox;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject paperPanel;
    [SerializeField] GameObject Joystick;
    public Joystick joystick;
    public void Pause(){
        PanelMenu.SetActive(true);
        Time.timeScale=0;
        Joystick.SetActive(false);
    }

    public void Resume(){
        PanelMenu.SetActive(false);
        Time.timeScale=1;
        Joystick.SetActive(true);
        if(Dialogbox.activeSelf){
            Time.timeScale=0f;
            Joystick.SetActive(false);
        }
        if (dialoguePanel.activeSelf)
        {
            Time.timeScale = 0f;
            Joystick.SetActive(false);
        }
        if(paperPanel.activeSelf)
        {
            Time.timeScale = 0f;
            Joystick.SetActive(false);
        }
    }

    public void Setting(){
        SettingMenu.SetActive(true);
        Joystick.SetActive(false);
    }
    public void Restart(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Time.timeScale=1;
    Joystick.SetActive(true);
    }
    
    public void BackButton(){
        SettingMenu.SetActive(false);
        Joystick.SetActive(false);
    }
}
