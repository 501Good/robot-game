using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReviveController : MonoBehaviour
{
    private void Awake()
    {
        Events.OnRespawnPlayer += RespawnPlayer;
    }

    private void OnDisable()
    {
        Events.OnRespawnPlayer -= RespawnPlayer;
    }

    private void RespawnPlayer()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        this.gameObject.GetComponent<PlayerController2D>().enabled = true;
    }
}
