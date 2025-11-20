using UnityEngine;

public class SwitchCameras : MonoBehaviour
{
    
    //switch between main camera behind the player and cameras in front of the keypads

    public Camera mainCamera;
    public Camera keypadCamera; 
    

    // void Start()
    // {
    //     // Ensure main camera is active at start
    //     ActivateMainCamera();
        
    // }

    // call when additively loading the levels
    public bool GetKeypadCamera()
    {
        keypadCamera = GameObject.Find("keypad-cam").GetComponent<Camera>();

        if (keypadCamera == null)
        {
            Debug.LogWarning("Keypad camera not found in the scene.");
            return false;
        }
        return true;
    }

    public void SwitchToKeypadCamera(bool switchToKeypad)
    {
        // switch to keypad is true when mainCamera is enable and false otherwise
        keypadCamera.enabled = switchToKeypad;
        mainCamera.enabled = !switchToKeypad;

    }

    // when to switch back to main camera? 
    // when close enough to keypad and pressing the Q button
}
