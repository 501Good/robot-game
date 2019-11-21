using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBossRocketLeapAttackState : ISpeedBossState
{
    private float curveWait;
    private bool curveStarted;

    private SpeedBossController parent;

    public void Enter(SpeedBossController parent)
    {
        this.parent = parent;
        curveWait = 1.1f;
        curveStarted = false;
    }

    public void Exit()
    {

    }

    private int rocketSpawnMaxCD = 4;
    private float rocketTimer = 0;
    

    public void Update()
    {
        if (rocketTimer <= 0)
        {
            GameObject.Instantiate(parent.rocketPrefab, parent.transform.position, Quaternion.identity);
            System.Random rand = new System.Random();
            rocketTimer = rand.Next(1, rocketSpawnMaxCD) / 10f;
        }
        else
            rocketTimer -= Time.deltaTime;



        if (!curveStarted)
        {
            if (Mathf.Abs(this.parent.transform.position.x - this.parent.PlatformLeftConstraint.transform.position.x) < 0.05f) // On left platform
                this.parent.StartCurve(true, Headings.leapOver);
            else // On right platform
                this.parent.StartCurve(false, Headings.leapOver);
            curveStarted = true;
        }
        curveWait -= Time.deltaTime;
        if(curveWait < 0)
        {
            this.parent.ChangeState(new SpeedBossActiveState());
        }

    }
}
