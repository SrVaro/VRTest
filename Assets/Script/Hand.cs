using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{

    // Animation
    Animator animator;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;  
    [SerializeField] float animationSpeed;
    private string animatorGripParam = "Grip";
    private string animatorTargetParam = "Trigger";

    // Physics Movement
    [Space]
    [SerializeField] private ActionBasedController controller;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [Space]
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    [Space]
    [SerializeField] private Transform palm;
    [SerializeField] private float reahDistance = 0.1f, joinDistance = 0.05f;
    [SerializeField] private LayerMask grabbableLayer;

    private bool isGrabbing = false;
    private GameObject heldObject;
    private Transform grabPoint;
    private FixedJoint joint1, joint2;

    private Transform followTarget;
    private Rigidbody body;


    void Start()
    {
        //Animation
        animator = GetComponent<Animator>();

        // Physics Movement
        followTarget = controller.gameObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;
        body.maxAngularVelocity = 20f;

        // Input Setup
        controller.selectAction.action.started += Grab;
        controller.selectAction.action.canceled += Release;

        // Teleport hands
        body.position = followTarget.position;
        body.rotation = followTarget.rotation;

    }

    private void Release(InputAction.CallbackContext obj)
    {
        if (joint1 != null) Destroy(joint1);
        if (joint2 != null) Destroy(joint2);
        if (grabPoint != null) Destroy (grabPoint.gameObject);

        if (heldObject != null) 
        {
            var targetBody = heldObject.GetComponent<Rigidbody>();
            targetBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            targetBody.interpolation = RigidbodyInterpolation.None;
            heldObject = null;
        }

        isGrabbing = false;
        followTarget = controller.gameObject.transform;
    }

    private void Grab(InputAction.CallbackContext ctx)
    {

        if(isGrabbing || heldObject) return;
        
        Collider[] grabbableColliders = Physics.OverlapSphere(palm.position, reahDistance, grabbableLayer);

        if(grabbableColliders.Length < 1) return;

        var objectToGrab = grabbableColliders[0].transform.gameObject;

        var objectBody = objectToGrab.GetComponent<Rigidbody>();

        if (objectBody !=null) 
        {
            heldObject = objectBody.gameObject;
        } 
        else 
        {
            objectBody = objectToGrab.GetComponentInParent<Rigidbody>();
            if (objectBody != null)
            {
                heldObject = objectBody.gameObject;
            } else 
            {
                return;
            }
        }

        StartCoroutine(GrabObject(grabbableColliders[0], objectBody));
    }

    private IEnumerator GrabObject(Collider collider, Rigidbody targetBody)
    {

        isGrabbing = true;

        // Create a grab point
        grabPoint = new GameObject().transform;
        grabPoint.position = collider.ClosestPoint(palm.position);
        grabPoint.parent = heldObject.transform;

        // Move hand to grab point
        followTarget = grabPoint;

        // Wait for to reach grab point
        while(Vector3.Distance(grabPoint.position, palm.position) > joinDistance && isGrabbing) 
        {
            yield return new WaitForEndOfFrame();
        }

        // Freeze hand and object motion
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        targetBody.velocity = Vector3.zero;
        targetBody.angularVelocity = Vector3.zero;

        targetBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        targetBody.interpolation = RigidbodyInterpolation.Interpolate;

        // Attach joints
        joint1 = gameObject.AddComponent<FixedJoint>();
        joint1.connectedBody = targetBody;
        joint1.breakForce = float.PositiveInfinity;
        joint1.breakTorque = float.PositiveInfinity;

        joint1.connectedMassScale = 1;
        joint1.massScale = 1;
        joint1.enableCollision = false;
        joint1.enablePreprocessing = false;

        joint2 = heldObject.AddComponent<FixedJoint>();
        joint2.connectedBody = body;
        joint2.breakForce = float.PositiveInfinity;
        joint2.breakTorque = float.PositiveInfinity;

        joint2.connectedMassScale = 1;
        joint2.massScale = 1;
        joint2.enablePreprocessing = false;        
        joint2.enableCollision = false;

        // Reset follow target
        followTarget = controller.gameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();

        PhysicsMove();
    }

    private void PhysicsMove()
    {
        // Position
        var positionWithOffset = followTarget.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance * Time.deltaTime);

        // Rotation
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(transform.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle > 180.0f) angle -= 360.0f;
        body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed * Time.deltaTime);

    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    void AnimateHand() 
    {
        if(gripCurrent != gripTarget) 
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
        if(triggerCurrent != triggerTarget) 
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorTargetParam, triggerCurrent);
        }
    }
}
