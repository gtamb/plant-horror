using UnityEngine;
using TMPro;
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
        if ((!sOneConsumed || !sTwoConsumed) && cInternalDistance < 2.0f)
        {
            // display prompt to consume object

            // check for button press
            if (Raycasting.layerName == "Specimen" && Input.GetKeyDown(KeyCode.E))
            {
                //isConsumed = true;
                ConsumeObject(Raycasting.hitTag);
                Destroy(Raycasting.lastHitObject);
            }
        }

        // if object is consumed disable script on object
    }

    // find which object is being consumed 
    // then consume the the object and apply its effects 
    public void ConsumeObject(string hitTag)
    {
    //    Debug.Log("Object Consumed", string(hitTag));

        if (hitTag == "specimen-one")
        {
            
            // apply specimen 1 effects
            pm.sOneConsumed = true;
            Debug.Log("Specimen 1 Consumed");
        }
        else if (hitTag == "specimen-two")
        {
            sTwoConsumed = true;
            pm.sTwoConsumed = true;
            // apply specimen 2 effects
            Debug.Log("Specimen 2 Consumed");
        }



    }
}
