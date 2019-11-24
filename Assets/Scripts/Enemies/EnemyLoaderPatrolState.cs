using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoaderPatrolState : IEnemyState
{
    private EnemyController parent;

    private float speed;
    private float myWidth;

    public void Enter(EnemyController parent)
    {
        this.parent = parent;
        this.speed = parent.speed;
        this.myWidth = this.parent.GetComponent<SpriteRenderer>().bounds.extents.x;
        Flip(parent.lookingRight);
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        parent.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

        // Platform ground edge check
        Vector2 LineCastPos = parent.transform.position - parent.transform.right * myWidth;
        Debug.DrawLine(LineCastPos, LineCastPos + Vector2.down * 3);
        bool isgrounded = Physics2D.Linecast(LineCastPos, LineCastPos + Vector2.down * 2, parent.groundMask);
        if (!isgrounded) Flip();

        // Running into wall check
        Vector2 WallLineCastPos = parent.transform.position - parent.transform.right * myWidth;
        Debug.DrawLine(WallLineCastPos, WallLineCastPos + Vector2.right * 0.2f);
        bool isWall = Physics2D.Linecast(LineCastPos, LineCastPos + Vector2.right * 0.2f, parent.groundMask);
        if (isWall) Flip();

        // Player detection
        RaycastHit2D[] hitList = Physics2D.RaycastAll(parent.transform.position, parent.transform.right * -1, 12);
        Debug.DrawLine(parent.transform.position, parent.transform.position + parent.transform.right * -1 * 12);

        bool foundPlayer = false;
        foreach (RaycastHit2D hit in hitList)
        {
            if (hit.collider.CompareTag("Player"))
            {
                foundPlayer = true;
                break;
            }
        }
        if (foundPlayer)
            parent.ChangeState(new EnemyLoaderChaseState());
    }


    void Flip()
    {
        if (this.parent.transform.localRotation.eulerAngles.y == 0)
        {
            speed = -speed;
            parent.lookingRight = true;
            this.parent.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            speed = -speed;
            parent.lookingRight = false;
            this.parent.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void Flip(bool lookingRight)
    {
        if (lookingRight)
        {
            speed = -Mathf.Abs(speed);
            parent.lookingRight = true;
            this.parent.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            speed = Mathf.Abs(speed);
            parent.lookingRight = false;
            this.parent.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
