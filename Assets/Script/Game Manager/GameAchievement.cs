using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAchievement : MonoBehaviour
{
    public int[] riwayatAchievement;
    
    void Start()
    {
        LoadRiwayatAchievement();
    }

    void LoadRiwayatAchievement(){
        for(int i = 0;i <riwayatAchievement.Length;i++){
            riwayatAchievement[i]=PlayerPrefs.GetInt($"achievement{i}", 0);
        }
    }
   

    void Update()
    {
        
    }
}
