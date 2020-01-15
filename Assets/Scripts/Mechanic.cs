using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();
    }

    void FacePlayer()
    {
        //Debug.Log(Vector3.Dot(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position));
        Vector3 posDiff = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        if (posDiff.x < 0)
        {
            transform.right = new Vector3(-1.0f, 0f, 0f);
        } else
        {
            transform.right = new Vector3(1.0f, 0f, 0f);
        }
        //transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
}
