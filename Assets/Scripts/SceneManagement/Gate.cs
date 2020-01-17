using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public string gateName = "";
    public string otherGate = "";
    public string otherScene = "";

    public GameObject player;

    public bool isOccupied;

    public void Awake()
    {
        this.gameObject.name = gateName;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Action postLoad = () =>
        {
            Debug.Log("Postload started!");
            Debug.Log("Name: " + gateName);
            Debug.Log("otherGate: " + otherGate);
            Debug.Log("otherScene: " + otherScene);
            if (string.IsNullOrEmpty(otherGate)) {
                Debug.Log("No otherGate");
                return; 
            }
            Debug.Log("Finding other gate");
            Gate newGate = GameObject.Find(otherGate).GetComponent<Gate>();
            Debug.Log("New gate: " + newGate);
            newGate.SetOccupied();
            Debug.Log("Player position: " + other.transform.position);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            other.transform.position = new Vector3(35f, 0f, 0f);
            Debug.Log("New gate position: " + newGate.transform.position);
            Debug.Log("Collision: " + player.gameObject);
            Debug.Log("Player position: " + player.transform.position);
            Debug.Log("Postload finished");
        };
        
        StartCoroutine(SceneChanger.ChangeScene(otherScene, this.gameObject.scene.name, postLoad));
    }

    void SetOccupied()
    {
        isOccupied = true;
    }
}
