using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Bullet BulletPrefab;

    private PlayerController2D playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Shooting");
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        Bullet bullet = GameObject.Instantiate<Bullet>(BulletPrefab, null);
        bullet.transform.position = this.transform.position;
        bullet.SetForwardDir(playerController.GetFacingDirection());
        
        
    }
}
