using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    private Animator animator;
    //private CapsuleCollider enemyCollider;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //enemyCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (health <= 0)
        //    Destroy(gameObject);
    }

    public void UpdateHealth(float damage)
    {
        if (!animator.GetBool("isDead"))
        {
            health -= damage;
            ManageAnimationStates(health);



            Debug.Log("Enemy health: " + health);
        }
    }

    private void ManageAnimationStates(float health)
    {
        if (health > 0)
        {
            animator.SetBool("getHit", true);
        }
        else if (health <= 0)
        {
            animator.SetBool("getHit", false);
            animator.SetBool("isDead", true);

            //enemyCollider.enabled = false;
        }
    }

    public void StopGetHitAnimation()
    {
        animator.SetBool("getHit", false);
        Debug.Log("STOPPED");
    }

    public void StopDieAnimation()
    {
        Destroy(gameObject, 5f);
        Debug.Log("Test");
    }
}
