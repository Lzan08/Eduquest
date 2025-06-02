using UnityEngine;
using TMPro;

public class MissionPanelManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject missionPanel; 
    [SerializeField] private TMP_Text missionText;   

    [Header("Mission Data")]
    [SerializeField] private Mission[] missions;     
    private int currentMissionIndex = -1;           

    [System.Serializable]
    public class Mission
    {
        public GameObject giver;         
        public string description;       
        public GameObject target;        
    }

    
    public void StartMission(int missionIndex)
    {
        if (missionIndex < 0 || missionIndex >= missions.Length)
        {
            return;
        }

        currentMissionIndex = missionIndex;
        missionText.text = missions[missionIndex].description;
        missionPanel.SetActive(true); 
    }

   
    public void StartMissionByGiver(GameObject giver)
    {
        for (int i = 0; i < missions.Length; i++)
        {
            if (missions[i].giver == giver)
            {
                StartMission(i);
                return;
            }
        }
    }

    
    public void UpdateMissionProgress(GameObject target)
    {
        if (currentMissionIndex == -1) return;

        if (missions[currentMissionIndex].target == target)
        {
            CompleteMission();
        }
    }

   
    private void CompleteMission()
    {
        missionPanel.SetActive(false); 
        currentMissionIndex = -1;     
    }
}
