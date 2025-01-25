using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightChanger : MonoBehaviour
{
    public Light light; // Reference to the Light component
    public InputActionReference action; // Reference to the Input Action

    // Start is called before the first frame update
    void Start()
    {
        if (light == null)
        {
            light = GetComponent<Light>();
            if (light == null)
            {
                Debug.LogError("No Light component found on this GameObject.");
                return;
            }
        }

        // Ensure the action is enabled
        action.action.Enable();
        action.action.performed += ChangeLightColor;
    }

    private void ChangeLightColor(InputAction.CallbackContext ctx)
    {
        light.color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
    }

    void OnDestroy()
    {
        action.action.performed -= ChangeLightColor;
    }
}
