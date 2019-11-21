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

    private void Awake()
    {
        Events.OnPlayerChangeHealth += PlayerChangeHealth;
        Events.OnPlayerRequestHealth += PlayerRequestHealth;
    }

    private void OnDestroy()
    {
        Events.OnPlayerChangeHealth -= PlayerChangeHealth;
        Events.OnPlayerRequestHealth -= PlayerRequestHealth;
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
}
