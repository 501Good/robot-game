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
        Vector3 destination = parent.MiddleConstraint.position;
        parent.transform.position = Vector3.MoveTowards(parent.transform.position, destination, 4f * Time.deltaTime);
    }

}
