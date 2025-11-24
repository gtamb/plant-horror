using UnityEngine;

public class Raycasting : MonoBehaviour
{
    public static float distanceFromTarget;
    [SerializeField] float toTarget;
    public static string layerName; 
    public static string hitTag;
    public static GameObject lastHitObject;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            toTarget = hit.distance;
            distanceFromTarget = hit.distance;

            int hitLayer = hit.collider.gameObject.layer;
            layerName = LayerMask.LayerToName(hitLayer);
            hitTag = hit.collider.gameObject.tag;
            lastHitObject = hit.collider.gameObject;

        }
    }
}
