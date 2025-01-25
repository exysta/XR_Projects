using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scope : MonoBehaviour
{
public Animator animator;
public InputActionReference action;

private Boolean isScoped = false;
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("No Light component found on this GameObject.");
                return;
            }
        }

        // Ensure the action is enabled
        action.action.Enable();
        action.action.performed += Scoping;
    }

    private void Scoping(InputAction.CallbackContext ctx)
    {
        isScoped = !isScoped;
        animator.SetBool("scoped",isScoped); 
    }
}
