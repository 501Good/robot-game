using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBatController : MonoBehaviour
{
    public BatRock rockController;

    public GameObject player;

    public bool HasRock = true;




    private void Update()
    {
        //Move to player on X axis
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(player.transform.position.x,transform.position.y, transform.position.z), 2f* Time.deltaTime);

        //If has rock and over player, drop it
        if (HasRock && Mathf.Abs(transform.position.x - player.transform.position.x) < 0.01f)
            DropRock();

    }


    private void DropRock()
    {
        rockController.GetComponent<BatRock>().enabled = true;
        HasRock = false;
    }













}
