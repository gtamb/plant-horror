using UnityEngine;
using TMPro;
public class InteractWithObjs : MonoBehaviour
{
   [SerializeField] float cInternalDistance;
   [SerializeField] bool isConsumed = false; // for the consumable specimens
   void Update()
    {
        // check for distance to consumable objects 
        // if internal distance and button pressed consume object
        cInternalDistance = Raycasting.distanceFromTarget;
        //check if internal distance is within bounds
        // !! need to check that the object being looked at is consumable !!
        if (isConsumed == false && cInternalDistance < 2.0f)
        {
            // display prompt to consume object

            // check for button press
            if (Input.GetKeyDown(KeyCode.E))
            {
                isConsumed = true;
                ConsumeObject();
            }
        }

        // if object is consumed disable script on object
    }

    // find which object is being consumed 
    // then consume the the object and apply its effects 
    public void ConsumeObject()
    {
        Debug.Log("Object Consumed");


    }
}
