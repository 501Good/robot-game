using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_platform : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool broken;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        broken = false;
    }

    public void Break(int x, int y)
    {
        broken = true;
        rb.isKinematic = false;
        rb.AddForce(new Vector2(x,y),ForceMode2D.Impulse);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnBecameInvisible()
    {
        if(broken)
            Destroy(this.gameObject);
    }
}
