using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private string[] attackAnimations;
    private int attackAnimationCounter;

    public void Attack(Animator playerAnimator, float attackRange, float damage)
    {


        Debug.Log(attackAnimationCounter);
        playerAnimator.Play(attackAnimations[attackAnimationCounter]);

        Vector3 rayStartPosition = new Vector3(
            transform.position.x,
            transform.position.y + 1f,
            transform.position.z);

        if (Physics.Raycast(rayStartPosition, transform.forward, out RaycastHit hit, attackRange))
        {
            //Debug.Log("Hitting " + hit.transform.name);
            //Debug.DrawRay(startPosition, transform.forward, new Color(1, 0, 0, 1f));

            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
                enemy.UpdateHealth(damage);
        }
    }

    public void StopAttackAnimation(Animator playerAnimator)
    {
        playerAnimator.SetBool("isAttacking", false);
    }

    public void UpdateAttackAnimCounter()
    {
        attackAnimationCounter = (attackAnimationCounter + 1) % attackAnimations.Length;        
    }

    public void ResetAtackAnimCounter()
    {
        // Has to be -1 because as soon as we click it's being incremented to 0
        attackAnimationCounter = -1;
    }
}
