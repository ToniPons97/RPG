using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float acceleration = 2f;
    [SerializeField] float stopDistance = 3f;
    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private void Awake()
    {

    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;

        animator = GetComponent<Animator>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
      
        navMeshAgent.speed = speed;
        navMeshAgent.acceleration = acceleration;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        //&& !animator.GetBool("isDead")
        if (distance > stopDistance )
        {
            animator.SetBool("isWalking", true);
            navMeshAgent.SetDestination(playerTransform.position);
            transform.LookAt(playerTransform);
 
        }
        else
        {
            animator.SetBool("isWalking", false);
            navMeshAgent.velocity = Vector3.zero;
        }

    }
}
