using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class Health : MonoBehaviour
{
    [SerializeField]
    private int health = 20;

    public TextMeshProUGUI text;
 
    private Rigidbody2D rb;

    private void Start()
    {
        if (this.CompareTag("Player"))
        {
            text.text = "HP : " + health;
        }

        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int dam)
    {
        health -= dam;

        if (this.CompareTag("Player"))
        {
            text.text = "HP : " + health;
        }

        if (health <= 0) this.gameObject.SendMessage("Death");
    }
}
