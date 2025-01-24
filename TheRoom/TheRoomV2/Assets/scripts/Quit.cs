using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;

public class Quit : MonoBehaviour
{
    // Reference to the Input Action
    public InputActionReference action;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the action is enabled
        action.action.Enable();

        // Subscribe to the performed event
        action.action.performed += (ctx) =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        };
    }


}
