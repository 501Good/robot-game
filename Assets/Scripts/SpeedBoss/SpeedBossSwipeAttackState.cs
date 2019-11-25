using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpeedBossSwipeAttackState : ISpeedBossState
{
    private SpeedBossController parent;

    private Transform Target;

    private float Speed = 18f;
    
    public void Enter(SpeedBossController parent)
    {
        this.parent = parent;
        parent.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Exit()
    {
        parent.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Update()
    {
        Speed += Time.deltaTime * 2;
        if (Target == null)
            Target = GetTarget();
        if (Vector2.Distance(this.parent.transform.position, Target.position) < 0.05f)
            parent.ChangeState(new SpeedBossActiveState());
        else
            this.parent.transform.position = Vector2.MoveTowards(this.parent.transform.position, Target.position, Speed * Time.deltaTime);
    }

    private Transform GetTarget()
    {
        float distanceToLeft = Vector2.Distance(this.parent.transform.position, this.parent.GroundLeftConstraint.position);
        float distanceToRight = Vector2.Distance(this.parent.transform.position, this.parent.GroundRightConstraint.position);

        if (distanceToLeft > distanceToRight)
            return this.parent.GroundLeftConstraint;
        else
            return this.parent.GroundRightConstraint;
    }

    
}
