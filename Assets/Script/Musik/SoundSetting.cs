using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;
    public void setmusicsetting(){
        float volume = BGMSlider.value;
        myMixer.SetFloat("BGM",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("BGMVolume",volume);
    }
    public void setSFXsetting(){
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume",volume);


    }
    void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume")){
            LoadVolume();
        }else{
            setmusicsetting();
            setSFXsetting();
        }
    }

    

    private void LoadVolume(){
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        setmusicsetting();
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        setSFXsetting();
    }
}
