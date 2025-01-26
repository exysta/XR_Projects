using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrabV2 : MonoBehaviour
{
    CustomGrabV2 otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    public bool doubleRotation = false; // Feature toggle for doubling rotation
    private bool grabbing = false;

    private Vector3 lastControllerPosition;
    private Quaternion lastControllerRotation;

    private void Start()
    {
        action.action.Enable();

        // Initialize last position and rotation
        lastControllerPosition = transform.position;
        lastControllerRotation = transform.rotation;

        // Find the other hand
        foreach (CustomGrabV2 c in transform.parent.GetComponentsInChildren<CustomGrabV2>())
        {
            if (c != this)
                otherHand = c;
        }
    }

    void Update()
    {
        grabbing = action.action.IsPressed();

        if (grabbing)
        {
            // Grab object if not already grabbing
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            {
                // Calculate delta position and rotation for this controller
                Vector3 deltaPosition = transform.position - lastControllerPosition;
                Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(lastControllerRotation);

                // Optionally double the rotation
                if (doubleRotation)
                {
                    deltaRotation = DoubleQuaternionRotation(deltaRotation);
                }

                // Apply the controller's translation and rotation to the grabbed object
                Vector3 objectOffset = grabbedObject.position - transform.position;
                grabbedObject.position += deltaPosition + deltaRotation * objectOffset - objectOffset;
                grabbedObject.rotation = deltaRotation * grabbedObject.rotation;

                // If both hands are grabbing the same object, combine the deltas
                if (otherHand && otherHand.grabbedObject == grabbedObject)
                {
                    // Calculate deltas from the other hand
                    Vector3 otherDeltaPosition = otherHand.transform.position - otherHand.lastControllerPosition;
                    Quaternion otherDeltaRotation = otherHand.transform.rotation * Quaternion.Inverse(otherHand.lastControllerRotation);

                    // Optionally double the other hand's rotation
                    if (otherHand.doubleRotation)
                    {
                        otherDeltaRotation = DoubleQuaternionRotation(otherDeltaRotation);
                    }

                    // Combine translations
                    grabbedObject.position += otherDeltaPosition;

                    // Combine rotations
                    grabbedObject.rotation = otherDeltaRotation * grabbedObject.rotation;
                }
            }
        }
        else if (grabbedObject)
        {
            // Release the object
            grabbedObject = null;
        }

        // Update last position and rotation for the next frame
        lastControllerPosition = transform.position;
        lastControllerRotation = transform.rotation;
    }

    private Quaternion DoubleQuaternionRotation(Quaternion rotation)
    {
        // Double the angle of rotation while preserving the axis
        float angle;
        Vector3 axis;
        rotation.ToAngleAxis(out angle, out axis);
        return Quaternion.AngleAxis(angle * 2f, axis);
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
            nearObjects.Remove(t);
    }
}
