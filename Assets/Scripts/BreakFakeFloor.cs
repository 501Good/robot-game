using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakFakeFloor : MonoBehaviour
{
    public GameObject FakenWall;
    public GameObject FakenLava;
    public GameObject FakenLavaCollider;
    public List<GameObject> fakeFloors;
    public GameObject bridgeOb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            bridgeOb.SetActive(true);
            BreakFloor();
            Destroy(FakenWall);
            Destroy(FakenLava);
            Destroy(FakenLavaCollider);
            Destroy(this.gameObject);
        }
    }

    private void BreakFloor()
    {
        foreach (GameObject go in fakeFloors)
        {
            go.GetComponent<Breakable_platform>().Break(1, 1);
        }
    }

}
