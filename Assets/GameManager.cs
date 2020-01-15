using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _playerHealth = 100;
    
    private int _playerMaxHealth = 100;
    private bool playerAllowedTransformation = false;
    public Image PlayerHealthPresenter;
    public GameObject cameras;
    private bool playerAlive = true;
     
    public Checkpoint[] Checkpoints;
    public Checkpoint LastActiveCheckpoint;

    public GameObject respawnPanel;

    public PlayerController2D CurrentCharacter;
    public Cinemachine.CinemachineVirtualCamera publicCamera;

    public static bool IsInputEnabled = true;

    private void Awake()
    {
        Events.OnPlayerChangeHealth += PlayerChangeHealth;
        Events.OnPlayerRequestHealth += PlayerRequestHealth;
        Events.OnChangeToCamera += ChangeToCamera;

        Events.OnChangeAllowTransformation += ChangeAllowTransformation;
        Events.OnRequestAllowTransformation += RequestAllowTransformation;
        Events.OnPlayerDeath += playerDeath;

        Events.OnSetCurrentCharacter += SetCurrentCharacter;
        Events.OnSetLastActiveCheckpoint += SetLastActiveCheckpoint;
        Events.OnRespawnPlayer += RespawnPlayer;
    }

    private void OnDestroy()
    {
        Events.OnPlayerChangeHealth -= PlayerChangeHealth;
        Events.OnPlayerRequestHealth -= PlayerRequestHealth;
        Events.OnChangeToCamera -= ChangeToCamera;

        Events.OnChangeAllowTransformation -= ChangeAllowTransformation;
        Events.OnRequestAllowTransformation -= RequestAllowTransformation;
        Events.OnPlayerDeath -= playerDeath;

        Events.OnSetCurrentCharacter -= SetCurrentCharacter;
        Events.OnSetLastActiveCheckpoint -= SetLastActiveCheckpoint;
        Events.OnRespawnPlayer -= RespawnPlayer;

    }
    
    private void PlayerChangeHealth(int value)
    {
        _playerHealth = value;
        PlayerHealthPresenter.fillAmount = (float)_playerHealth/_playerMaxHealth;
        if(_playerHealth <= 0)
        {
            Events.PlayerDeath();
        }
    }

    private void playerDeath()
    {
        playerAlive = false;
        respawnPanel.SetActive(true);
    }
    
    private int PlayerRequestHealth()
    {
        return _playerHealth;
    }

    private void RespawnPlayer()
    {
        CurrentCharacter.transform.position = LastActiveCheckpoint.transform.position;
        PlayerChangeHealth(100);
        playerAlive = true;

        foreach (Cinemachine.CinemachineVirtualCamera cam in cameras.transform.GetComponentsInChildren<Cinemachine.CinemachineVirtualCamera>())
        {
            cam.gameObject.SetActive(false);
        }
        publicCamera.gameObject.SetActive(true);
        respawnPanel.SetActive(false);

    }

    private void SetCurrentCharacter(PlayerController2D character)
    {
        CurrentCharacter = character;
    }

    private void SetLastActiveCheckpoint(Checkpoint checkpoint)
    {
        LastActiveCheckpoint = checkpoint;
    }

    private void ChangeToCamera(Cinemachine.CinemachineVirtualCamera targetCam)
    {
        foreach(Cinemachine.CinemachineVirtualCamera cam in cameras.transform.GetComponentsInChildren<Cinemachine.CinemachineVirtualCamera>())
        {
                cam.gameObject.SetActive(false);
        }
        targetCam.gameObject.SetActive(true);
    }

    private void ChangeAllowTransformation(bool value)
    {
        playerAllowedTransformation = value;
    }

    private bool RequestAllowTransformation()
    {
        return playerAllowedTransformation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            respawnPanel.SetActive(!respawnPanel.activeSelf);
            if (respawnPanel.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
