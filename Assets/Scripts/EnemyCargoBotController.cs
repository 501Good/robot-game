using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCargoBotController : MonoBehaviour
{
    [SerializeField]
    private int health = 4;

    public void TakeDamage(int dam)
    {
        health -= dam;

        if (health <= 0) Destroy(this.gameObject);
    }
}
