using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatAttackState : IEnemyState
{
    private EnemyController parent;
    private float timer;


    public void Enter(EnemyController parent)
    {
        this.parent = parent;
        timer = 1f;
        var currentRock = GameObject.Instantiate(parent.BatRockPrefab, parent.transform.position + new Vector3(0,-1,0), Quaternion.identity);

    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (timer > 0f)
            timer -= Time.deltaTime;
        else
        {
            parent.ChangeState(new EnemyBatPatrolState());
        }
    }
}
