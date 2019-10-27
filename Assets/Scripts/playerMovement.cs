using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 2f;

    [SerializeField]
    private float jumpForce = 300f;

    [SerializeField]
    private Transform firingPosition;

    [SerializeField]
    private HitEffect hitEffect;

    private bool facingRight = true;

    private Rigidbody2D rigidbody2D;
    Vector3 prevPos = Vector3.zero;

    [SerializeField]
    private float invincibleCD = 1f;
    private float invincibleTimer = 0f;
    private bool invincible = false;

    [SerializeField]
    private int health = 100;

    private Animator animator;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(Input.GetAxisRaw("Horizontal"),0,0), movementSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown("k")) Flip();
        if (Input.GetKeyDown("h")) ShootGun();

        if (Input.GetAxis("Horizontal") > 0f && !facingRight)
            Flip();
        else if (Input.GetAxis("Horizontal") < 0f && facingRight)
            Flip();

        if (Input.GetButtonUp("Horizontal"))
            animator.SetBool("Walking", false);
        if (Input.GetButtonDown("Horizontal"))
            animator.SetBool("Walking", true);
        

        if (invincibleTimer > 0f)
            invincibleTimer -= Time.deltaTime;
        else if(invincible)
        {
            invincible = false;
        }
    }

    void Jump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    void Flip()
    {
        if (facingRight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }
        
    }

    void resetshoot()
    {
        animator.ResetTrigger("Shoot");
    }

    void ShootGun()
    {

        animator.SetTrigger("Shoot");
        Invoke("resetshoot", 0.2f);
        Vector2 direction = new Vector2(1, 0);
        if(facingRight) direction = new Vector2(1, 0);
        else direction = new Vector2(-1, 0);
        RaycastHit2D hit = Physics2D.Raycast(firingPosition.position, firingPosition.transform.right,5f);
        Debug.Log("Shoot");
        //Debug.DrawLine(firingPosition.position, firingPosition.position + new Vector3(10,0,0), Color.green,2f);
        
        if(hit)
        {
            Debug.Log(hit.collider.name);
            Instantiate(hitEffect, hit.point, Quaternion.identity);
            if (hit.collider.CompareTag("Enemy")) hit.collider.SendMessageUpwards("TakeDamage", 10);
        }
        else
        {
            Instantiate(hitEffect, firingPosition.position + firingPosition.right * 5, Quaternion.identity);
        }

        
    }

    void TakeDamage(int dmg)
    {
        if (!invincible)
        {
            this.health -= dmg;
            invincible = true;
            invincibleTimer = invincibleCD;
        }
    }

    void Death()
    {
        Debug.Log("You're dead");
    }
}
