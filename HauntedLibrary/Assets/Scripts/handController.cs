using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInputDetection : MonoBehaviour
{
    public Hand hand;

    // Using InputActionProperty lets you assign the action directly in the Inspector.
    [SerializeField]
    public InputActionReference triggerAction;

    [SerializeField]
    public InputActionReference gripAction;


    private void Update()
    {

    // Read the current float value of the trigger and grip.
        float triggerValue = triggerAction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();

        hand.SetTrigger(triggerValue);
        hand.SetGrip(gripValue);

/*        if (triggerValue > 0.01)
        {
            Debug.Log($"Trigger: {triggerValue:F2}");
        }
        if (gripValue > 0.01)
        {
            Debug.Log($"grip: {gripValue:F2}");
        }*/
    }
}
