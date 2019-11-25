using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWormAttackState : IEnemyState
{
    private EnemyController parent;
    private Rigidbody2D rb;

    private bool attacked;
    private float timer;

    private GameObject AttackTrigger;

    public void Enter(EnemyController parent)
    {
        this.parent = parent;
        rb = parent.GetComponent<Rigidbody2D>();
        attacked = false;
        timer = 0.5f;
        AttackTrigger = parent.transform.GetChild(0).gameObject;
        AttackTrigger.SetActive(true);
    }

    public void Exit()
    {
        AttackTrigger.SetActive(false);
    }

    public void Update()
    {
        timer -= Time.deltaTime;

        if (!attacked)
        {
            rb.AddForce(parent.transform.up * 4,ForceMode2D.Impulse);
            rb.AddForce(parent.transform.right*-10, ForceMode2D.Impulse);
            attacked = true;
        }
        else
        {
            if(timer < 0 && rb.velocity.y == 0)
            {
                parent.ChangeState(new EnemyWormPatrolState());
            }
        }
    }
}
