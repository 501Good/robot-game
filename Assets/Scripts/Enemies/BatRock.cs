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
            Death();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !breaking)
        {
            breaking = true;
            Break();
            collision.SendMessage("TakeDamage", damage);
        }
        else if(collision.CompareTag("Ground") && !breaking)
        {
            breaking = true;
            Break();
        }
    }


    private void Break()
    {
        if(animator)
            animator.SetBool("Broken", true);
        Invoke("Death", 1f);

    }

    void Death()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(int dam)
    {
        breaking = true;
        Break();
    }


}
