using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBatController : MonoBehaviour
{

    public GameObject player;

    public LayerMask groundMask;

    public bool HasRock = false;
    public float RockCD = 4f;
    private float RockTimer = 4f;

    [SerializeField]
    private float bobHeight = 1f;
    private float bobPosition = 0f;

    [SerializeField]
    private Transform RockSpawnPosition;

    [SerializeField]
    private GameObject RockPrefab;

    private GameObject currentRock;

    private Vector3 rockPos = Vector3.zero;

    float targetX;


    [SerializeField]
    private bool playerFound = false;

    private void Start()
    {
        rockPos = RockSpawnPosition.localPosition;
    }

    private void Update()
    {
        //  Checking if player has been found ( in range to activate follow )
        if (!playerFound && Mathf.Abs(player.transform.position.x - transform.position.x) < 10f)
            playerFound = true;
        if (!playerFound)
            targetX = transform.position.x;
        else
            targetX = player.transform.position.x;

        //If has rock and over player, drop it
        if (HasRock && Mathf.Abs(transform.position.x - player.transform.position.x) < 0.01f)
            DropRock();

        //If there is no rock, it will be spawned after cd
        if (!HasRock && RockTimer > 0f)
            RockTimer -= Time.deltaTime;
        else if (!HasRock && RockTimer <= 0f)
        {
            HasRock = true;
            RockTimer = RockCD;
            currentRock = Instantiate(RockPrefab, this.transform.position,Quaternion.identity);
            currentRock.transform.SetParent(this.transform);
            currentRock.transform.position = this.transform.position + rockPos;
        }
        
        // Bat bobbing up and down movement
        bobPosition -= Time.deltaTime;
        if (bobPosition <= -bobHeight)
            bobPosition = bobHeight;


        RaycastHit2D[] groundHit = Physics2D.RaycastAll(transform.position, Vector3.down, groundMask);
        float height = transform.position.y;
        foreach(RaycastHit2D hit in groundHit)
        {
            if (hit.transform.CompareTag("Ground"))
                height = hit.point.y + 5f;
        }

        float targetHeight = height + Mathf.Abs(bobPosition);
        Vector3 targetLocation = new Vector3(targetX, targetHeight, this.transform.position.z);
        //Move to player on X axis
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, 3f * Time.deltaTime);
    }


    private void DropRock()
    {
        currentRock.GetComponent<BatRock>().enabled = true;
        transform.GetComponentInChildren<BatRock>().gameObject.transform.SetParent(null);
        HasRock = false;
        currentRock = null;
        RockTimer = 4f;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
