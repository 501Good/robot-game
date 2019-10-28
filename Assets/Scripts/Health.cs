using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health = 20;

    public TextMeshProUGUI text;

    private void Start()
    {
        if (this.CompareTag("Player"))
        {
            text.text = "HP : " + health;
        }
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
