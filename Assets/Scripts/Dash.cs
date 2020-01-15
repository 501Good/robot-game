using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    public PlayerController2D playerController;

    public float DashCD = 0.75f;
    private float _DashTimer = 0.75f;
    public float DashPower;

    // Start is called before the first frame update
    void Start()
    {
        //playerController = GetComponent<PlayerController2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _DashTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && _DashTimer <= 0f)
        {
            
            //playerAudio.PlayShot();
            Dashing();
        }
    }

    void Dashing()
    {
        //playerController.GetFacingDirection()
        Vector3 forceToAdd;
        if (Input.GetKey(KeyCode.W))
        {
            forceToAdd = Vector2.up * DashPower;
            Debug.Log("W Dashing");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            forceToAdd = Vector2.left * DashPower;
            Debug.Log("A Dashing");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forceToAdd = Vector2.down * DashPower;
            Debug.Log("S Dashing");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            forceToAdd = Vector2.right * DashPower;
            Debug.Log("D Dashing");
        }
        else
        {
            Debug.Log("No Dashing");
            return;
        }
        //playerController.GetComponent<PlayerController2D>().GetComponent<Rigidbody2D>().AddForce(forceToAdd, ForceMode2D.Impulse);
       // playerController.GetComponent<Animator>().enabled = false;
        playerController.gameObject.transform.position += forceToAdd;
        //playerController.GetComponent<Animator>().enabled = true;
        _DashTimer = DashCD;
    }
}

