using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBoss : MonoBehaviour
{
    public GameObject laser1;
    public GameObject laser2;
    private bool laserOn = false;
    private float _Timer = 0;
    public float offsetX;

    public float pingpongMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _Timer += Time.deltaTime;
        transform.position = new Vector3(Mathf.PingPong(Time.time, pingpongMove) + offsetX, transform.position.y, transform.position.z);
        if (_Timer >= 1)
        {
            laserOn = !laserOn;
            _Timer = 0;
        }
        laser1.SetActive(laserOn);
        laser2.SetActive(laserOn);
    }
}
