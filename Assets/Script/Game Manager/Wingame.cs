using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wingame : MonoBehaviour
{
    [SerializeField] private GameObject triggerObj;

    void Start()
    {
        
        StartCoroutine(CheckTriggerObject());
    }

   
    IEnumerator CheckTriggerObject()
    {
        
        while (triggerObj != null)
        {
            yield return new WaitForSeconds(1f); 

            if (triggerObj == null) 
            {
                LoadNextScene();
                yield break;
            }
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

       
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        
    }
}
