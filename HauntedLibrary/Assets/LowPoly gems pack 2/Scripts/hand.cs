using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float speed;

    Animator animator;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;

    internal void SetGrip(float gripValue)
    {
        gripTarget = gripValue;
    }

    internal void SetTrigger(float triggerValue)
    {
        triggerTarget = triggerValue;
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat("Grip", gripCurrent);
        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat("Trigger", triggerCurrent);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            AnimateHand();
    }
}

