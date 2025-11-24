using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FireController : MonoBehaviour
{
    public float damagePerSecond = 5f;
    public float growthRate = 0.1f; // How fast the fire area grows
    public float maxScale = 5f;
    public float tickRate = 1f; // Damage interval in seconds

    private HashSet<Collider> objectsInZone = new HashSet<Collider>();
    private float damageTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ApplyDamageOverTime());
        StartCoroutine(GrowFireArea());
        
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject.name + " entered fire area.");
        if (other.GetComponent<PlayerHealth>() != null)
        {
            objectsInZone.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Debug.Log(other.gameObject.name + " exited fire area.");
        if (other.GetComponent<PlayerHealth>() != null)
        {
            objectsInZone.Remove(other);
        }
    }

    IEnumerator ApplyDamageOverTime()
    {
        while (true) // Keep running while the fire exists
        {
            yield return new WaitForSeconds(tickRate);
            foreach (Collider collider in objectsInZone)
            {
                if (collider != null)
                {
                    //apply damage to current in PlayerHealth script
                    PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeFireDamage(Mathf.RoundToInt(damagePerSecond));
                    }
                }
            }
        }
    }

    IEnumerator GrowFireArea()
    {
        while (transform.localScale.x < maxScale)
        {
            transform.localScale += new Vector3(growthRate * Time.deltaTime, growthRate * Time.deltaTime, growthRate * Time.deltaTime);
            yield return null; // Wait until next frame
        }
    }
}

// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic; // Required for a list or dictionary of objects in the zone

// public class FireDamageZone : MonoBehaviour
// {
//     public float damagePerTick = 10f;
//     public float tickRate = 1f; // Damage interval in seconds
//     public float growthRate = 1f; // How fast the fire area grows
//     public float maxScale = 10f; // Max size of the fire area

//     private HashSet<Collider> objectsInZone = new HashSet<Collider>();
//     private float damageTimer;

//     void Start()
//     {
//         // Start the damage-over-time and growth coroutines
//         StartCoroutine(ApplyDamageOverTime());
//         StartCoroutine(GrowFireArea());
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         // Add objects with health component to the tracking list
//         if (other.GetComponent<PlayerHealth>() != null)
//         {
//             objectsInZone.Add(other);
//         }
//     }

//     void OnTriggerExit(Collider other)
//     {
//         // Remove objects when they leave the zone
//         if (objectsInZone.Contains(other))
//         {
//             objectsInZone.Remove(other);
//         }
//     }

//     IEnumerator ApplyDamageOverTime()
//     {
//         while (true) // Keep running while the fire exists
//         {
//             yield return new WaitForSeconds(tickRate);
//             foreach (Collider collider in objectsInZone)
//             {
//                 if (collider != null)
//                 {
//                     PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
//                     if (playerHealth != null)
//                     {
//                         playerHealth.TakeDamage(Mathf.RoundToInt(damagePerTick));
//                     }
//                 }
//             }
//         }
//     }

//     IEnumerator GrowFireArea()
//     {
//         while (transform.localScale.x < maxScale)
//         {
//             transform.localScale += new Vector3(growthRate * Time.deltaTime, growthRate * Time.deltaTime, growthRate * Time.deltaTime);
//             yield return null; // Wait until next frame
//         }
//     }
// }