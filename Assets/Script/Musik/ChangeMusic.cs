using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{

    public int indexMusic;
    void Start()
    {
        if (GameObject.Find("AudioManager")!=null){

        }
    AudioManager.Instance.ChangeBGM(indexMusic);
    }
}
