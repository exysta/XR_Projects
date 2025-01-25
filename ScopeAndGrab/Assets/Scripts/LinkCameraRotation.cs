using UnityEngine;

public class LinkCameraRotation : MonoBehaviour
{
    // Reference to the first camera
    [SerializeField] private Transform camera1;
    [SerializeField] private Transform lens;

    void Update()
    {
        if (camera1 != null)
        {
            // Match the rotation of Camera 2 (this object) to Camera 1
            transform.rotation = camera1.rotation;
            transform.position = lens.position;

        }
    }


}
