using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChangeControl : MonoBehaviour
{

    //controls the scene changes and will load and unload the different areas

    // start with adding the first lab scene to the player scene
    private bool foundKCam;
    //public string sceneName;
    public void Start()
    {
        LoadScene("Lab-Scene");
    }
    // call with correct 
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        foundKCam = FindObjectOfType<SwitchCameras>().GetKeypadCamera();
        if (foundKCam != null)
        {
            Debug.Log("Found keypad camera after loading scene: " + sceneName);
        }
        else
        {
            Debug.LogWarning("Keypad camera not found after loading scene: " + sceneName);
        }
    }
    // call after player has moved into the new area -> have doors close after they are through the area then unload
    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
