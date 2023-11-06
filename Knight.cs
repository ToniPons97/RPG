using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Knight : MonoBehaviour
{
    [SerializeField] private Transform[] targets;

    private NavMeshAgent navMeshAgent;

    [Range(0.1f, 3f)][SerializeField] private float minSpeed;
    [Range(0.1f, 3f)] [SerializeField] private float maxSpeed;

    [Range(0.1f, 3f)] [SerializeField] private float minAcceleration;
    [Range(0.1f, 3f)] [SerializeField] private float maxAcceleration;

    private Animator animator;

    private float distance;
    private int counter;
    private float stopDistance;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        float randomSpeed = Random.Range(minAcceleration, maxAcceleration);

        navMeshAgent.speed = randomSpeed;
        navMeshAgent.acceleration = Random.Range(minSpeed, maxSpeed);

        counter = 0;
        stopDistance = Random.Range(5f, 20f);

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, targets[counter].position);

        navMeshAgent.SetDestination(targets[counter].position);

        Debug.Log("Speed: " + navMeshAgent.acceleration);

        if (distance > stopDistance)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }


        if (distance < stopDistance)
        {
            counter = (counter + 1) % targets.Length;
            //Debug.Log(name + ": " + distance);
        }
    }
}
