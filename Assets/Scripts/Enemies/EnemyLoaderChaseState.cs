using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoaderChaseState : IEnemyState
{
    private EnemyController parent;
    private GameObject player;
    
    private float speed;

    private float playerDetectCD;
    private float playerDetectTimer;

    private float myWidth;

    public void Enter(EnemyController parent)
    {
        this.parent = parent;
        this.speed = parent.speed * 2;
        playerDetectCD = 3f;
        playerDetectTimer = 3f;
        this.myWidth = this.parent.GetComponent<SpriteRenderer>().bounds.extents.x;
        if (parent.lookingRight)
        {
            speed = -speed;
        }
    }

    public void Exit()
    {

    }

    public void Update()
    {
        playerDetectTimer -= Time.deltaTime;
        if (playerDetectTimer < 0)
            parent.ChangeState(new EnemyLoaderPatrolState());

        // Player Detection
        RaycastHit2D[] hitList = Physics2D.RaycastAll(parent.transform.position, parent.transform.right * -1, 12);
        Debug.DrawLine(parent.transform.position, parent.transform.position + parent.transform.right * -1 * 12);
        bool foundPlayer = false;
        foreach (RaycastHit2D hit in hitList)
        {
            if (hit.collider.CompareTag("Player"))
            {
                foundPlayer = true;
                player = hit.transform.gameObject;
                break;
            }
        }
        if (foundPlayer)
            playerDetectTimer = playerDetectCD;

        if (player != null)
        {
            // Platform ground edge check
            Vector2 LineCastPos = parent.transform.position - parent.transform.right * myWidth;
            Debug.DrawLine(LineCastPos, LineCastPos + Vector2.down * 3);
            bool isgrounded = Physics2D.Linecast(LineCastPos, LineCastPos + Vector2.down * 2, parent.groundMask);

            // Running into wall check
            Vector2 WallLineCastPos = parent.transform.position - parent.transform.right * myWidth;
            Debug.DrawLine(WallLineCastPos, WallLineCastPos + Vector2.right * 0.2f);
            bool isWall = Physics2D.Linecast(LineCastPos, LineCastPos + Vector2.right * 0.2f, parent.groundMask);

            if (!parent.lookingRight && player.transform.position.x - parent.transform.position.x > 0)
                Flip();
            else if (parent.lookingRight && player.transform.position.x - parent.transform.position.x < 0)
                Flip();

            if (isgrounded && !isWall)
            {
                parent.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }

            if (Mathf.Abs(parent.transform.position.x - player.transform.position.x) <= 1)
                parent.ChangeState(new EnemyLoaderAttackState());
        }

    }
    void Flip()
    {
        if (this.parent.transform.localRotation.eulerAngles.y == 0)
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
