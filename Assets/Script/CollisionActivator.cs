using UnityEngine;

public class CollisionActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectsToActivate; 
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActivateAllGameObjects();
        }
    }

    private void ActivateAllGameObjects()
    {
        foreach (GameObject obj in gameObjectsToActivate)
        {
            obj.SetActive(true);
        }
    }
}
