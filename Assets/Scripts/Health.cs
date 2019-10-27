using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health = 20;


    public void TakeDamage(int dam)
    {
        health -= dam;

        if (health <= 0) this.gameObject.SendMessage("Death");
    }
}
