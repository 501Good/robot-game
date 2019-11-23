using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;

    public void SetForwardDir(float forwardDir)
    {
        switch (forwardDir)
        {
            case 0:
                //Shoot up

                break;
            case 1:
                //Shoot to the Right
                transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
                break;
            case -1:
                //Shoot to the Left
                transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
                break;
            case 2:
                //Shoot down
                transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
                break;
        }

        
        //this.transform.position += new Vector3(transform.up.x, transform.up.y * 0.7f, transform.up.z);
    }

    void Update()
    {
        this.transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            this.speed = 0;

            this.gameObject.GetComponent<Animator>().SetTrigger("BlowUp");
        }
        Debug.Log(collision.tag);
        //var hp = collision.GetComponent<Health>();

        
        if (/*hp != null && */!collision.CompareTag("Player"))
        {
            collision.SendMessage("TakeDamage", 10);
            this.speed = 0;
            this.gameObject.GetComponent<Animator>().SetTrigger("BlowUp");
        }
    }


}
