using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class InteractWithObjs : MonoBehaviour
{
   [SerializeField] float cInternalDistance;
   [SerializeField] bool isConsumed = false; // for the consumable specimens

    [SerializeField] PlayerMovement pm;
    public bool sOneConsumed;
    public bool sTwoConsumed;
    void Start()
    {
       pm = FindObjectOfType<PlayerMovement>();

        // fallback: find by tag "Player" and get the component
        if (pm == null)
        {
            GameObject playerGO = GameObject.FindWithTag("Player");
            if (playerGO != null) pm = playerGO.GetComponent<PlayerMovement>();
        }
        sOneConsumed = pm.sOneConsumed;
        sTwoConsumed = pm.sTwoConsumed;

    }
   void Update()
    {
        // check for distance to consumable objects 
        // if internal distance and button pressed consume object
        cInternalDistance = Raycasting.distanceFromTarget;
        
        //check if internal distance is within bounds
        bool canEat = Raycasting.lastHitObject != null &&
                  Raycasting.layerName == "Specimen" &&
                  cInternalDistance < 2.0f && 
                  IsAlreadyConsumed(Raycasting.hitTag) == false;
   
        if (pm.eatPrompt != null)
            pm.eatPrompt.SetActive(canEat);
        
        if (canEat && Input.GetKeyDown(KeyCode.E))
        {
            //isConsumed = true;
            ConsumeObject(Raycasting.hitTag);
            Destroy(Raycasting.lastHitObject);
            
        }

        // if object is consumed disable script on object
    }

    // find which object is being consumed 
    // then consume the the object and apply its effects
    private bool IsAlreadyConsumed(string hitTag)
    {
        switch(hitTag)
        {
            case "specimen-one": return pm.sOneConsumed;
            case "specimen-two": return pm.sTwoConsumed;
            case "specimen-three": return false; // maybe handled differently
            default: return true; // unknown objects are not consumable
        }
    } 
    public void ConsumeObject(string hitTag)
    {
    //    Debug.Log("Object Consumed", string(hitTag));

        if (hitTag == "specimen-one")
        {
            
            // apply specimen 1 effects
            pm.sOneConsumed = true;
            pm.dJumpPrompt.SetActive(true);
            pm.eatPrompt.SetActive(false);
            Debug.Log("Specimen 1 Consumed");
        }
        else if (hitTag == "specimen-two")
        {
            sTwoConsumed = true;
            pm.sTwoConsumed = true;
            pm.glidePrompt.SetActive(true);
            pm.eatPrompt.SetActive(false);

            // apply specimen 2 effects
            Debug.Log("Specimen 2 Consumed");
        }
        else if(hitTag == "specimen-three")
        {
            // apply specimen 3 effects
            Debug.Log("Specimen 3 Consumed");
            // maybe more effects for being eaten later
            SceneManager.UnloadSceneAsync("Lab-Area-2");
            SceneManager.UnloadSceneAsync("Player");
            SceneManager.UnloadSceneAsync("Mother-Plant-Area");
            SceneManager.LoadScene("EndScene");
        }



    }
}
