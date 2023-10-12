using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float acceleration = 2f;
    private NavMeshAgent navAgent;

    private Camera cam;
    private Ray castRay;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navAgent.speed = speed;
        navAgent.acceleration = acceleration;
        cam = Camera.main;

        // Get animator from child game object.
        animator = gameObject.transform
            .GetChild(0)
            .gameObject
            .GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }

        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        castRay = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(castRay, out RaycastHit hit))
        {
            navAgent.destination = hit.point;
        }
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = navAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("forwardSpeed", speed);

    }



}
