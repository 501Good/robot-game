using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoaderAttackState : IEnemyState
{
    private EnemyController parent;

    private GameObject punchingFist;
    private Animator animator;
    private float punchingTime;
    private bool punched;

    public void Enter(EnemyController parent)
    {
        this.parent = parent;
        punchingFist = parent.transform.GetChild(0).gameObject;
        animator = parent.GetComponent<Animator>();
        punchingFist.SetActive(true);
        punchingTime = 0.5f;
    }

    public void Exit()
    {
        punchingFist.SetActive(false);
        animator.SetBool("Punching", false);
    }

    public void Update()
    {
        animator.SetBool("Punching", true);
  
        if (punchingTime > 0f)
            punchingTime -= Time.deltaTime;
        else
            parent.ChangeState(new EnemyLoaderPatrolState());
    }
}
