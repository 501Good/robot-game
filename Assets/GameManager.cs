using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _playerHealth = 100;
    
    private int _playerMaxHealth = 100;
    public Image PlayerHealthPresenter;

    public GameObject cameras;
    public Checkpoint[] Checkpoints;
    public Checkpoint LastActiveCheckpoint;

    public PlayerController2D CurrentCharacter;

    private void Awake()
    {
        Events.OnPlayerChangeHealth += PlayerChangeHealth;
        Events.OnPlayerRequestHealth += PlayerRequestHealth;
        Events.OnChangeToCamera += ChangeToCamera;
        Events.OnSetCurrentCharacter += SetCurrentCharacter;
        Events.OnSetLastActiveCheckpoint += SetLastActiveCheckpoint;
        Events.OnRespawnPlayer += RespawnPlayer;
    }

    private void OnDestroy()
    {
        Events.OnPlayerChangeHealth -= PlayerChangeHealth;
        Events.OnPlayerRequestHealth -= PlayerRequestHealth;
        Events.OnChangeToCamera -= ChangeToCamera;
        Events.OnSetCurrentCharacter -= SetCurrentCharacter;
        Events.OnSetLastActiveCheckpoint -= SetLastActiveCheckpoint;
        Events.OnRespawnPlayer -= RespawnPlayer;
    }
    
    private void PlayerChangeHealth(int value)
    {
        _playerHealth = value;
        PlayerHealthPresenter.fillAmount = (float)_playerHealth/_playerMaxHealth;
    }

    private int PlayerRequestHealth()
    {
        return _playerHealth;
    }

    private void RespawnPlayer()
    {
        CurrentCharacter.transform.position = LastActiveCheckpoint.transform.position;
        PlayerChangeHealth(100);
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
}
