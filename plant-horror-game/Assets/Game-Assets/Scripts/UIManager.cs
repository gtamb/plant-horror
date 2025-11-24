using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        SceneManager.LoadScene("Player");
        SceneManager.LoadScene("Lab-Scene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}

