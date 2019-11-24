using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatPatrolState : IEnemyState
{
    private EnemyController parent;

    private GameObject player;
    private float bobHeight = 1f;
    private float bobPosition = 0f;
    private float targetX;
    private float speed;

    public void Enter(EnemyController parent)
    {
        this.parent = parent;
        player = null; //Events.RequestPlayerGameObject().transform.gameObject;
        speed = parent.speed;
    }

    public void Exit()
    {

    }

    public void Update()
    {
        if(player == null)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(parent.transform.position, 15f);
            foreach(Collider2D col in cols)
            {
                if (col.CompareTag("Player"))
                    player = col.gameObject;
            }
        }
        targetX = parent.transform.position.x;

        if(player != null && Vector3.Distance(parent.transform.position,player.transform.position) < 15)
        {
            targetX = player.transform.position.x;
        }

        // Bat bobbing up and down movement
        bobPosition -= Time.deltaTime;
        if (bobPosition <= -bobHeight)
            bobPosition = bobHeight;

        bool playerFound = false;
        RaycastHit2D[] groundHit = Physics2D.RaycastAll(parent.transform.position, Vector3.down, parent.groundMask);
        float height = parent.transform.position.y;
        foreach (RaycastHit2D hit in groundHit)
        {
            if (hit.transform.CompareTag("Ground"))
                height = hit.point.y + 5f;
        }

        float targetHeight = height + Mathf.Abs(bobPosition);
        Vector3 targetLocation = new Vector3(targetX, targetHeight, parent.transform.position.z);
        //Move to player on X axis
        parent.transform.position = Vector3.MoveTowards(parent.transform.position, targetLocation, speed * Time.deltaTime);

        if(player != null)
        {
            if (Mathf.Abs(parent.transform.position.x - player.transform.position.x) <= 0.01f)
                parent.ChangeState(new EnemyBatAttackState());
        }
    }
}
