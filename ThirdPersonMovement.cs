using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ThirdPersonMovement : MonoBehaviour
{
    private Transform cam;
    private CharacterController controller;
    [SerializeField] private float speed = 6f;
    [Range(2f, 3f)] [SerializeField] private float boosterSpeed;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Animator animator;
    [SerializeField] private float smoothAnimExit = 0.2f;
    [SerializeField] private float groundCheckLength = 10f;

    [SerializeField] private float damage = 0.1f;
    [SerializeField] private float attackRange = 2f;
    private Fighter fighter;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        cam = Camera.main.transform;
        fighter = GetComponent<Fighter>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            fighter.Attack(animator, attackRange, damage);
        }
        else
        {
            fighter.StopAttackAnimation(animator);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f && !animator.GetBool("isAttacking"))
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            float booster = Input.GetKey(KeyCode.LeftShift) ? boosterSpeed : 1;

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            Vector3 move = booster * speed * Time.deltaTime * moveDirection.normalized;

            // Ground character.
            // Perform raycasting to detect ground.
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckLength))
            {
                move.y = -hit.distance; // Keep the character grounded.
            }
            else
            {
                move.y = 0f; // Prevent hovering in the air if the ray doesn't hit.
            }

            controller.Move(move);
            animator.SetFloat("forwardSpeed", direction.magnitude * booster, smoothAnimExit, Time.deltaTime);
        }
        else
        {
            animator.SetFloat("forwardSpeed", 0f, smoothAnimExit, Time.deltaTime);
        }
    }
}
