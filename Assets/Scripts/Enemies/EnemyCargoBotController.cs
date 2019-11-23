using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCargoBotController : MonoBehaviour
{
    // Player Detection
    [SerializeField]
    private bool playerDetected = false;
    [SerializeField]
    private float playerDetectCD = 4f;
    [SerializeField]
    private float playerDetectTimer = 4f;
    private GameObject player = null;

    // Moving
    public LayerMask groundMask;
    public float speed;
    private Transform MyTrans;
    private float MyWidth;

    // Punching variables
    public float punchingDistance = 0.5f;
    public bool punching = false;
    public float punchCD = 2f;
    private float punchTimer = 0f;
    private float punchStopDelay = 0f;
    [SerializeField]
    private GameObject punchingArmGO;


    private Animator animator;


    private void Start()
    {
        MyWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        MyTrans = this.transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Player detecting in looking direction
        RaycastHit2D[] hitList = Physics2D.RaycastAll(transform.position, MyTrans.right * - 1);
        Debug.DrawLine(transform.position, MyTrans.position + MyTrans.right * -1);

        bool foundPlayer = false;
        foreach(RaycastHit2D hit in hitList)
        {
            if (hit.collider.CompareTag("Player"))
            {
                playerDetected = true;
                foundPlayer = true;
                playerDetectTimer = playerDetectCD;
                if (player == null)
                    player = hit.transform.gameObject;
            }
        }
        if (!foundPlayer) playerDetected = false;
        playerDetectTimer -= Time.deltaTime;

        if (punchStopDelay > 0f)
            punchStopDelay -= Time.deltaTime;
        else
        {
            punching = false;
            animator.SetBool("Punching", false);
            punchingArmGO.SetActive(false);
        }

        if (punchTimer > 0f)
            punchTimer -= Time.deltaTime;



        // Moving mechanics, will stop if punching
        if (!punching)
        {
            if (playerDetected && playerDetectTimer > 0) // If player is being followed
            {
                float playerX = player.transform.position.x;
                float myX = this.transform.position.x;

                // If looking right but player is left, flip
                if (transform.localRotation.eulerAngles.y == 180 && playerX < myX)
                    Flip();
                // If looking left but player is right, flip
                else if (transform.localRotation.eulerAngles.y == 0 && playerX > myX)
                    Flip();

                // Checking player distance, if close it will prompt attack
                if (Mathf.Abs(playerX - myX) <= punchingDistance)
                {
                    if(punchTimer <= 0f)
                    {
                        Punch();
                    }
                }
                else
                {
                    transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                }
            }
            else // If player is not being followed aka patrolling
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }
        
        
    }

    private void FixedUpdate()
    {
        // Grounded and wall detection

        // Platform ground edge check
        Vector2 LineCastPos = MyTrans.position - MyTrans.right * MyWidth;
        Debug.DrawLine(LineCastPos, LineCastPos + Vector2.down);
        bool isgrounded = Physics2D.Linecast(LineCastPos, LineCastPos + Vector2.down * 2, groundMask);
        if (!isgrounded) Flip();

        // Running into wall check
        Vector2 WallLineCastPos = MyTrans.position - MyTrans.right * MyWidth;
        Debug.DrawLine(WallLineCastPos, WallLineCastPos + Vector2.left);
        bool isWall = Physics2D.Linecast(LineCastPos, LineCastPos + Vector2.left * 0.2f, groundMask);
        if (isWall) Flip();
    }

    void Flip()
    {
        if (transform.localRotation.eulerAngles.y == 0)
        {
            speed = -speed;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        else
        {
            speed = -speed;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }


    private void Punch()
    {
        punchStopDelay = 1f;
        punching = true;
        animator.SetBool("Punching", true);
        punchTimer = punchCD;
        punchingArmGO.SetActive(true);
    }


    void Death()
    {
        Destroy(this.gameObject);
    }
}
