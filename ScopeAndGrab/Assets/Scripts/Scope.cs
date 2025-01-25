using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scope : MonoBehaviour
{
public Animator animator;
public InputActionReference action;
public GameObject scopeOverlay;
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
        scopeOverlay.SetActive(isScoped);
        if (isScoped)
            StartCoroutine(OnScope());
        else
            OnUnscoped();
    }

    void OnUnscoped()
    {
        scopeOverlay.SetActive(false);

    }

    IEnumerator OnScope()
    {
        yield return new WaitForSeconds(.2f);
        scopeOverlay.SetActive(true);

    }
}
