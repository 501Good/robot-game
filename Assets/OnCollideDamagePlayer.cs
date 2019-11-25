using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideDamagePlayer : MonoBehaviour
{
    private bool damaged;
    public int damage;

    private void OnEnable()
    {
        damaged = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!damaged && collision.CompareTag("Player"))
        {
            damaged = true;
            Events.PlayerChangeHealth(Events.PlayerRequestHealth() - damage);
        }
    }
}
