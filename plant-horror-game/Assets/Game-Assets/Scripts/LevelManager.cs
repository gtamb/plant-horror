using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public GameObject brokenDoor;
    [SerializeField] public GameObject doorTwo;
    [SerializeField] public Collider triggerZone;

    public bool singleUse = true;
    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (triggered && singleUse) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the door trigger zone.");
            // Open the broken door
            brokenDoor.SetActive(true);
            doorTwo.SetActive(true);
            SceneManager.UnloadSceneAsync("Lab-Scene");
        }
    }
}
