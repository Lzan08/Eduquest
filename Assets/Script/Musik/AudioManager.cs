using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioBGM;
    public AudioSource audioSFX;
    public static AudioManager Instance { get; set; }

    [Header("BGM")]
    public AudioClip[] clipMusik;

    [Header("SFX")]
    public AudioClip Benar;
    public AudioClip Salah;
    public AudioClip Klik;
    public AudioClip tabrak;

    private int currentBGMIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (audioBGM == null)
        {
            return;
        }

        if (clipMusik.Length == 0 || clipMusik[currentBGMIndex] == null)
        {
            return;
        }

        audioBGM.clip = clipMusik[currentBGMIndex];
        audioBGM.loop = true;
        audioBGM.Play();
    }

    public void ChangeBGM(int indexMusic)
    {
        if (indexMusic >= 0 && indexMusic < clipMusik.Length)
        {
            currentBGMIndex = indexMusic;
            if (clipMusik[currentBGMIndex] != null)
            {
                audioBGM.Stop();
                audioBGM.clip = clipMusik[currentBGMIndex];
                audioBGM.Play();
            }
        }
        
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            audioSFX.PlayOneShot(clip);
        }
        
    }
}
