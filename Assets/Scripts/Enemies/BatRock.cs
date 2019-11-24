using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRock : MonoBehaviour
{
    bool breaking = false;
    Animator animator;
    public int damage = 5;

    private float deathTimer = 6f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if(!breaking)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, 4f * Time.deltaTime);

        if (deathTimer > 0)
            deathTimer -= Time.deltaTime;
        else
            animator.SetTrigger("Break");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!breaking)
        {
            if (collision.CompareTag("Player"))
            {
                breaking = true;
                animator.SetTrigger("Break");
                Events.PlayerChangeHealth(Events.PlayerRequestHealth() - damage);
            }
            else if (collision.CompareTag("Ground"))
            {
                breaking = true;
                animator.SetTrigger("Break");
            }
        }
        
    }

    public void TakeDamage(int dam)
    {
        breaking = true;
        animator.SetTrigger("Break");
    }


}
