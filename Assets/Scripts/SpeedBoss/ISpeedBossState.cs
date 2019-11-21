using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpeedBossState
{
    void Enter(SpeedBossController parent);

    void Update();

    void Exit();
}