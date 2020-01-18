using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBoss : MonoBehaviour
{
    public GameObject laser1;
    public GameObject laser2;
    private bool laserOn = false;
    private float _Timer = 0;
    public float offsetX;

    public float pingpongMove;

    private bool _isAlive = true;
    private int _health;
    private int _maxHealth;
    public Image bossHealthImage;
    public GameObject BossHealthBar;

    public LaserBossAudio bossAudio;
    public Cinemachine.CinemachineVirtualCamera playerCamera;

    private void Start()
    {
        _maxHealth = 150;
        _health = _maxHealth;
    }

    private void OnEnable()
    {
        bossHealthImage.fillAmount = 1;
    }

    void Update()
    {
        if (_isAlive)
        {
            _Timer += Time.deltaTime;
            transform.position = new Vector3(Mathf.PingPong(Time.time, pingpongMove) + offsetX, transform.position.y, transform.position.z);
            if (_Timer >= 1)
            {
                laserOn = !laserOn;
                _Timer = 0;
                if (laserOn)
                {
                    bossAudio.PlayLaser();
                } else
                {
                    bossAudio.Stop();
                }
            }
            laser1.SetActive(laserOn);
            laser2.SetActive(laserOn);
        }
    }

    public void TakeDamage(int value)
    {
        _health -= value;
        bossHealthImage.fillAmount = (float)_health / _maxHealth;
        if (_health <= 0)
        {
            Death();
        }

    }

    private void Death()
    {

        _isAlive = false;
        laser1.SetActive(false);
        laser2.SetActive(false);
        Color changeColor = GetComponent<SpriteRenderer>().color;
        changeColor.a = 0.6f;
        GetComponent<SpriteRenderer>().color = changeColor;
        bossAudio.PlayDeath();
        Events.ChangeToCamera(playerCamera);
        BossHealthBar.SetActive(false);
    }
}
