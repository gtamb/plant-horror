using UnityEngine;

public class SwitchCameras : MonoBehaviour
{
    
    //switch between main camera behind the player and cameras in front of the keypads

    public GameObject player;
    public GameObject keypadCamera; 
    public bool KeypadUnlocked = false;
    public bool KeypadCameraActive = false;
    [SerializeField] float internalDistance;
    

    void Start()
    {
        // Ensure main camera is active at start
        player = GameObject.FindWithTag("Player");
       // keypadCamera = GameObject.FindWithTag("keypad-cam"); // took out because of how scene setup works
        
    }

    // call when additively loading the levels
    // public bool GetKeypadCamera()
    // {
    //     keypadCamera = GameObject.Find("keypad-cam").GetComponent<Camera>();

    //     if (keypadCamera == null)
    //     {
    //         Debug.LogWarning("Keypad camera not found in the scene.");
    //         return false;
    //     }
    //     return true;
    // }

    public void SwitchToKeypadCamera(bool switchToKeypad)
    {
        // switch to keypad is true when mainCamera is enable and false otherwise
        // !! turn off all cameras
        if(switchToKeypad == true)
        {
            player.SetActive(false);
            keypadCamera.SetActive(true);
        }
        else
        {
            keypadCamera.SetActive(false);
            player.SetActive(true);
        }


    }

    public void Update()
    {
        // Example input handling to switch cameras
        internalDistance = Raycasting.distanceFromTarget;
        if (KeypadUnlocked == false && internalDistance < 4.0f)
        {
            // switch to keypad camera
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Switching to Keypad Camera");
                KeypadCameraActive = true;
                SwitchToKeypadCamera(true);
            }

        }

        if(KeypadCameraActive == true)
        {
            // display back instructions
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Switching back to Main Camera");
                KeypadCameraActive = false;
                SwitchToKeypadCamera(false);
            }
        }
        
            
    }

    // when to switch back to main camera? 
    // when close enough to keypad and pressing the Q button
}
