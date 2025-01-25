using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPositionSwitcher : MonoBehaviour
{
    // Array of target positions and rotations for the camera's parent
    public Transform[] cameraPositions;

    // Reference to the input action for the XR controller button
    public InputActionReference switchCameraAction;

    // Index of the current target position
    private int currentIndex = 0;

    void OnEnable()
    {
        // Enable the input action
        switchCameraAction.action.Enable();
        // Subscribe to the performed event
        switchCameraAction.action.performed += OnSwitchCamera;
    }

    void OnDisable()
    {
        // Unsubscribe from the performed event
        switchCameraAction.action.performed -= OnSwitchCamera;
        // Disable the input action
        switchCameraAction.action.Disable();
    }

    void OnSwitchCamera(InputAction.CallbackContext context)
    {
        // Increment the index to switch to the next position
        currentIndex = (currentIndex + 1) % cameraPositions.Length;

        // Move the camera's parent to the new position and rotation
        transform.position = cameraPositions[currentIndex].position;
        transform.rotation = cameraPositions[currentIndex].rotation;

        Debug.Log($"Switched to position: {currentIndex}");
    }
}
