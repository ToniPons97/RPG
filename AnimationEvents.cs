using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void StopAttackAnimation()
    {
        // For some reason this is being called twice.
        animator.SetBool("isAttacking", false);
    }
}
