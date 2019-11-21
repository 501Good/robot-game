using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpeedBossIdleState : ISpeedBossState
{
    private SpeedBossController parent;

    public void Enter(SpeedBossController parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.K))
            parent.ChangeState(new SpeedBossActiveState());
    }

}
