using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Bullet BulletPrefab;

    private PlayerController2D playerController;
    private SpriteRenderer playerSpriteRenderer;

    public float ShootingCD = 1f;
    private float _shootingTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _shootingTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && _shootingTimer <= 0f)
        {
            Debug.Log("Shooting");
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        Bullet bullet = GameObject.Instantiate<Bullet>(BulletPrefab, null);
        bullet.transform.position = this.transform.position + new Vector3(0, 0.55f, 0);
        bullet.SetForwardDir(playerController.GetFacingDirection());

        _shootingTimer = ShootingCD;
    }
}
