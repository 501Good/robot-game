using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartTrigger : MonoBehaviour
{
    public GameObject breakables;
    public GameObject light;
    private bool started = false;
    public GameObject Boss;

    private void Start()
    {
        var temp = Boss.GetComponent<SpriteRenderer>().color;
        temp.a = 0;
        Boss.GetComponent<SpriteRenderer>().color = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!started && collision.CompareTag("Player"))
        {
            started = true;
            light.SetActive(true);
            breakAll();
            var temp = Boss.GetComponent<SpriteRenderer>().color;
            temp.a = 1;
            Boss.GetComponent<SpriteRenderer>().color = temp;
        }
    }

    private void breakAll()
    {
        System.Random rand = new System.Random();
        foreach (Breakable_platform plat in breakables.GetComponentsInChildren<Breakable_platform>())
        {
            int x = rand.Next(-5, 5);
            int y = rand.Next(0, 10);
            plat.Break(x,y);
        }
    }
}
