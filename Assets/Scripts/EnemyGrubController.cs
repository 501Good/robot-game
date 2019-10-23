using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrubController : MonoBehaviour
{
    public LayerMask enemyMask;
    public float speed;
    Rigidbody2D rigidbody2D;
    Transform myTrans;
    float myWidth;

    [SerializeField]
    private int health = 20;

    private void Start()
    {
        myTrans = this.transform;
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    private void Update()
    {
        transform.position -= new Vector3(speed * Time.deltaTime,0,0);
    }

    void Flip()
    {
        if(transform.localRotation.eulerAngles.y == 0)
        {
            speed = -speed;

            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        else
        {
            speed = -speed;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        Vector2 LineCastPos = myTrans.position - myTrans.right * myWidth;
        Debug.DrawLine(LineCastPos, LineCastPos + Vector2.down);
        bool isgrounded = Physics2D.Linecast(LineCastPos, LineCastPos + Vector2.down *2, enemyMask);
        if (!isgrounded) Flip();
    }

    public void TakeDamage(int dam)
    {
        health -= dam;

        if (health <= 0) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) collision.SendMessageUpwards("TakeDamage", 10);
    }
}
