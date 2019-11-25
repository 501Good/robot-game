using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject Light;
    private bool isActive;

    private void Start()
    {
        isActive = false;
    }

    public bool IsActive()
    {
        return isActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Checkpoint");
            Light.SetActive(true);
            isActive = true;
            Events.SetLastActiveCheckpoint(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Events.RespawnPlayer();
        }
    }


}
