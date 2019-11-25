using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnPlayerButton : MonoBehaviour
{
    public Button button;

    private void Awake()
    {
        button.onClick.AddListener(respawnPlayer);
    }

    private void respawnPlayer()
    {
        Events.RespawnPlayer();
    }
}
