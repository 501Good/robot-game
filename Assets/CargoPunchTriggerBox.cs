using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoPunchTriggerBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) collision.SendMessage("TakeDamage", 10);
    }
}
