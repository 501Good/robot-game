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

    private int rocketSpawnMaxCD = 20;
    private float rocketTimer = 0f;
    

    public void Update()
    {
        if (rocketTimer <= 0)
        {
            GameObject.Instantiate(parent.rocketPrefab, parent.transform.position + new Vector3(0,-0.5f,0), Quaternion.identity);
            System.Random rand = new System.Random();
            rocketTimer = 0.1f;//rand.Next(1, rocketSpawnMaxCD) / 100f;
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
