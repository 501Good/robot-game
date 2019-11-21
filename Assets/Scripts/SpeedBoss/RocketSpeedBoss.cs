using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpeedBoss : MonoBehaviour
{


    private void Update()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, this.transform.position + Vector3.down, 15f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            Events.PlayerChangeHealth(Events.PlayerRequestHealth() - 20);
        }
    }
}
