using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRock : MonoBehaviour
{
    bool breaking = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if(!breaking)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, 4f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !breaking)
        {
            breaking = true;
            Break();
            /// Damage player
        }
    }


    private void Break()
    {
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
