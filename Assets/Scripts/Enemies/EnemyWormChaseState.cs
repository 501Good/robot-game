using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWormChaseState : IEnemyState
{
    private EnemyController parent;

    public void Enter(EnemyController parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {

    }

    public void Update()
    {

    }
}
