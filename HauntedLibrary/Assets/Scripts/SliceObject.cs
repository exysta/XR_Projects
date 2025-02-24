using UnityEngine;
using EzySlice;

public class BladeSlicer : MonoBehaviour
{
    [Tooltip("Material applied to the new cut surfaces.")]
    public Material crossSectionMaterial;

    [Tooltip("Minimum movement (in meters) to consider as a slicing motion.")]
    public float minSliceSpeed = 0.01f;

    [Tooltip("Force applied to the sliced pieces to simulate an explosion.")]
    public float explosionForce = 300f;

    [Tooltip("Radius of the explosion force effect.")]
    public float explosionRadius = 1.0f;

    // Previous frame's position of the blade
    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        // Update previous position at the end of each frame.
        previousPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only attempt to slice objects with the "Slicable" tag
        if (!other.gameObject.CompareTag("Slicable"))
            return;

        // Calculate the blade's motion vector since the last frame
        Vector3 currentPosition = transform.position;
        Vector3 sliceMotion = currentPosition - previousPosition;

        // Check that the blade is moving fast enough to be considered a slice
        if (sliceMotion.magnitude < minSliceSpeed)
            return;

        // Define the slicing plane using the blade’s movement.
        // Here, the normalized motion vector is used as the plane's normal,
        // and the current blade position as a point on the plane.
        Vector3 planeNormal = sliceMotion.normalized;
        Vector3 planePosition = currentPosition;

        // Attempt to slice the target object.
        GameObject target = other.gameObject;
        SlicedHull hull = target.Slice(planePosition, planeNormal, crossSectionMaterial);

        if (hull != null)
        {
            // Create the two sliced halves.
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);

            // Add physics components and explosion force to the new pieces.
            if (upperHull != null)
            {
                MeshCollider upperCollider = upperHull.AddComponent<MeshCollider>();
                upperCollider.convex = true;
                Rigidbody upperRb = upperHull.AddComponent<Rigidbody>();
                upperRb.AddExplosionForce(explosionForce, planePosition, explosionRadius);
            }
            if (lowerHull != null)
            {
                MeshCollider lowerCollider = lowerHull.AddComponent<MeshCollider>();
                lowerCollider.convex = true;
                Rigidbody lowerRb = lowerHull.AddComponent<Rigidbody>();
                lowerRb.AddExplosionForce(explosionForce, planePosition, explosionRadius);
            }

            // Optionally, add visual or audio effects here

            // Remove the original object from the scene.
            Destroy(target);
        }
    }
}
