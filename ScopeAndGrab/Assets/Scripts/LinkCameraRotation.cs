using UnityEngine;

public class LinkCameraRotation : MonoBehaviour
{
    // Reference to the first camera (main camera)
    [SerializeField] private Transform camera1;

    void Update()
    {
        if (camera1 != null)
        {
            // Get the direction vector from the lens to the main camera
            Vector3 direction = transform.position  - camera1.position;

            // Calculate a suitable point to look at by adding the direction vector to the lens position
            Vector3 pointToLookAt = transform.position + direction;

            // Make this object look at the calculated point
            transform.LookAt(pointToLookAt);


        }
    }
}
