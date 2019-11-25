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
     

    private void Awake()
    {
        Events.OnPlayerChangeHealth += PlayerChangeHealth;
        Events.OnPlayerRequestHealth += PlayerRequestHealth;
        Events.OnChangeToCamera += ChangeToCamera;
        Events.OnChangeAllowTransformation += ChangeAllowTransformation;
        Events.OnRequestAllowTransformation += RequestAllowTransformation;
        Events.OnPlayerDeath += playerDeath;
    }

    private void OnDestroy()
    {
        Events.OnPlayerChangeHealth -= PlayerChangeHealth;
        Events.OnPlayerRequestHealth -= PlayerRequestHealth;
        Events.OnChangeToCamera -= ChangeToCamera;
        Events.OnChangeAllowTransformation -= ChangeAllowTransformation;
        Events.OnRequestAllowTransformation -= RequestAllowTransformation;
        Events.OnPlayerDeath -= playerDeath;
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
    }
    
    private int PlayerRequestHealth()
    {
        return _playerHealth;
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
}
