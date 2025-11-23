using UnityEngine;

public class Raycasting : MonoBehaviour
{
    public static float distanceFromTarget;
    [SerializeField] float toTarget;
    public static string layerName; 

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            toTarget = hit.distance;
            distanceFromTarget = hit.distance;

            int hitLayer = hit.collider.gameObject.layer;
            layerName = LayerMask.LayerToName(hitLayer);

        }
    }
}
