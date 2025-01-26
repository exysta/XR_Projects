using UnityEngine;

public class LookAtUp : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target == null) return;

        // Calculate the direction vector from the object to the target
        Vector3 direction = target.position - transform.position;

        // Project the direction onto the XZ plane to ignore vertical differences
        direction.y = 0;

        // If the target is directly above or below, we don't rotate
        if (direction.magnitude > 0.01f)
        {
            // Make the object look in the flattened direction
            transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        }
    }
}