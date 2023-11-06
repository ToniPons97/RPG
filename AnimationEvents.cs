using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = transform.parent.gameObject.GetComponent<CharacterController>();
    }


    public void StopAttackAnimation()
    {
        // For some reason this is being called twice.
        animator.SetBool("isAttacking", false);
    }

    public void StopJumpAnimation()
    {
        animator.SetBool("isJumping", false);
    }

    public void StartJumpMotion(float jumpForce)
    {
        controller.Move(jumpForce * Time.deltaTime * Vector3.up);
    }
}
