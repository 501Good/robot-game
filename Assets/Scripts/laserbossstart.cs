using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserbossstart : MonoBehaviour
{
    public GameObject roomlight;
    public GameObject bluelightr;
    public GameObject bossObj;
    public GameObject bosscam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(bluelightr);
            roomlight.SetActive(true);
            bossObj.SetActive(true);
            bosscam.SetActive(true);
            Destroy(this.gameObject);
        }
    }


}
