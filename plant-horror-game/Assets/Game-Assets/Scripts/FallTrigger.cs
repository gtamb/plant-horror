using UnityEngine;
using UnityEngine.SceneManagement;
public class FallTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player fell to death");
            // Open the broken door
            SceneManager.LoadScene("RestartGame", LoadSceneMode.Single);
            // SceneManager.UnloadSceneAsync("Player");
            // SceneManager.UnloadSceneAsync("Lab-Area-2");
            // SceneManager.UnloadSceneAsync("Mother-Plant-Area");
            // SceneManager.LoadScene("RestartGame");
        }
    }
}
