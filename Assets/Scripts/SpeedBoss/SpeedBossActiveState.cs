using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum SpeedBossAttack
{
    FloorSwipe, RocketLeap, nothing
}

class SpeedBossActiveState : ISpeedBossState
{
    private SpeedBossController parent;
    private float attackTimer = 0.75f;

    private SpeedBossAttack ChosenAttack = SpeedBossAttack.nothing;
    private Transform Target = null;

    private bool onPlatform;

    private float waitCurve = 0f;



    ///////////////// State Enter ///////////////////////
    public void Enter(SpeedBossController parent)
    {
        this.parent = parent;
    }

    ///////////////// State Exit ////////////////////////
    public void Exit()
    {

    }

    ///////////////// State Update //////////////////////
    public void Update()
    {
        if (waitCurve > 0)
        {
            waitCurve -= Time.deltaTime;
        }
        else
        {
            // If there is no chosen attack, choose one at random
            if (ChosenAttack == SpeedBossAttack.nothing)
            {
                SpeedBossAttack[] attacks = { SpeedBossAttack.FloorSwipe, SpeedBossAttack.RocketLeap };
                System.Random random = new System.Random();
                ChosenAttack = attacks[random.Next(0, attacks.Length)];
                Debug.Log("ChosenAttack: " + ChosenAttack);
            }

            // Looks, if the player is on platform or not
            if (Math.Abs(parent.transform.position.y - parent.PlatformRightConstraint.transform.position.y) < 0.1f)
                onPlatform = true;
            else
                onPlatform = false;



            if (attackTimer > 0f)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                if (Target == null)
                    Target = GetTarget();

                

                // If on a platform and going to floorswipe, get off platform
                if(onPlatform && ChosenAttack == SpeedBossAttack.FloorSwipe) // Get off the platform if floorswipe
                {
                    waitCurve = 1.2f;
                    if (Target == this.parent.GroundLeftConstraint) // If heading left, must be on left platform
                    {
                        this.parent.StartCurve(true, Headings.jumpDown);
                    }
                    if(Target == this.parent.GroundRightConstraint) // If heading right, must be on right platform
                    {
                        this.parent.StartCurve(false, Headings.jumpDown);
                    }
                    return;
                }

                // If on the ground and going to leap over, get on platform
                if(!onPlatform && ChosenAttack == SpeedBossAttack.RocketLeap)
                {
                    if (Target == this.parent.PlatformLeftConstraint)
                    {
                        float distanceToLeap = Vector2.Distance(this.parent.transform.position, this.parent.PlatformLeftLeapConstraint.position);

                        if(distanceToLeap < 0.01f)
                        {
                            waitCurve = 1.2f;
                            this.parent.StartCurve(false, Headings.jumpUp);
                        }
                        else
                        {
                            this.parent.transform.position = Vector2.MoveTowards(this.parent.transform.position, this.parent.PlatformLeftLeapConstraint.position, 8f * Time.deltaTime);
                        }
                        
                    }

                    if (Target == this.parent.PlatformRightConstraint)
                    {
                        float distanceToLeap = Vector2.Distance(this.parent.transform.position, this.parent.PlatformRightLeapConstraint.position);

                        if (distanceToLeap < 0.01f)
                        {
                            waitCurve = 1.2f;
                            this.parent.StartCurve(true, Headings.jumpUp);
                        }
                        else
                        {
                            this.parent.transform.position = Vector2.MoveTowards(this.parent.transform.position, this.parent.PlatformRightLeapConstraint.position, 8f * Time.deltaTime);
                        }
                    }

                    return;
                }

                float distanceToTarget = Vector2.Distance(this.parent.transform.position, this.Target.position);

                if (distanceToTarget < 0.05f) // If arrived at target
                {
                    switch (ChosenAttack)
                    {
                        case SpeedBossAttack.RocketLeap:
                            this.parent.ChangeState(new SpeedBossRocketLeapAttackState());
                            break;
                        case SpeedBossAttack.FloorSwipe:
                            this.parent.ChangeState(new SpeedBossSwipeAttackState());
                            break;
                        default:
                            Debug.LogError("ChosenAttack on speedboss active state is error: " + ChosenAttack);
                            break;
                    }
                }
                else
                {
                    this.parent.transform.position = Vector2.MoveTowards(this.parent.transform.position, Target.position, 8f * Time.deltaTime);
                }
            }
        }
    }
    

    /*
     * Gets closer target constraint, left or right side on floor
     */
    private Transform GetTarget()
    {
        if(ChosenAttack == SpeedBossAttack.FloorSwipe)
        {
            float distanceToLeft = Vector2.Distance(this.parent.transform.position, this.parent.GroundLeftConstraint.position);
            float distanceToRight = Vector2.Distance(this.parent.transform.position, this.parent.GroundRightConstraint.position);

            if (distanceToLeft < distanceToRight)
                return this.parent.GroundLeftConstraint;
            else
                return this.parent.GroundRightConstraint;
        }


        if(ChosenAttack == SpeedBossAttack.RocketLeap)
        {
            float distanceToLeft = Vector2.Distance(this.parent.transform.position, this.parent.PlatformLeftConstraint.position);
            float distanceToRight = Vector2.Distance(this.parent.transform.position, this.parent.PlatformRightConstraint.position);

            if (distanceToLeft < distanceToRight)
                return this.parent.PlatformLeftConstraint;
            else
                return this.parent.PlatformRightConstraint;
        }


        return this.parent.MiddleConstraint;
    }
}