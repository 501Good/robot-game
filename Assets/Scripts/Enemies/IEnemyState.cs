using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Enter(EnemyController parent);

    void Update();

    void Exit();
}
