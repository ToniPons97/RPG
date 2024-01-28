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


    [SerializeField] private float gravity = 9.8f;
    private float verticalSpeed = 0f;
    [SerializeField] private float jumpForce = 10f;

    private bool isGrounded;



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
        GroundCheck();
        Move();
        Attack();
        Jump();
        ApplyGravity();
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


            controller.Move(move);
            animator.SetFloat("forwardSpeed", direction.magnitude * booster, smoothAnimExit, Time.deltaTime);
        }
        else
        {
            animator.SetFloat("forwardSpeed", 0f, smoothAnimExit, Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            fighter.Attack(animator, attackRange, damage);
        }
        else
        {
            fighter.StopAttackAnimation(animator);
        }

        if (Input.GetMouseButtonUp(0))
        {
            fighter.ResetAtackAnimCounter();
        }

    }

    private void Jump()
    {
        //Debug.Log("isGrounded: " + controller.isGrounded);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (isGrounded)
            {
                animator.SetBool("isJumping", true);

                //controller.Move(jumpForce * Time.deltaTime * Vector3.up);
            }
        }
    }

    private void ApplyGravity()
    {

        if (isGrounded)
        {
            verticalSpeed = 0f; // Reset vertical speed when grounded
        }
        else
        {
            verticalSpeed += gravity * Time.deltaTime;
        }

        Vector3 gravityVector = Vector3.down * verticalSpeed;

        // Apply gravity to the character controller's movement
        controller.Move(gravityVector * Time.deltaTime);
    }


    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckLength))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }


}
