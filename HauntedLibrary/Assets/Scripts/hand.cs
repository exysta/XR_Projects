using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float animationSpeed;

    Animator animator;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;

/*    // Physics Movement
    [SerializeField] private GameObject followObect;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    private Rigidbody body;
    private Transform followTarget;*/

    internal void SetGrip(float gripValue)
    {
        gripTarget = gripValue;
    }

    internal void SetTrigger(float triggerValue)
    {
        triggerTarget = triggerValue;
    }

/*    private void PhysicsMove()
    {
        //position

        var distance = Vector3.Distance(followTarget.position, transform.position);
        body.linearVelocity =(followTarget.position - transform.position).normalized * (followSpeed * distance);

    }*/

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat("Grip", gripCurrent);
        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat("Trigger", triggerCurrent);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

/*        //physics
        followTarget = followObect.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;

        // Teleport hands
        body.position = followTarget.position;
        body.rotation = followTarget.rotation;*/
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();

/*        PhysicsMove();
*/    }
}

