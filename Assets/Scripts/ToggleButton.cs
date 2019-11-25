using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleButton : MonoBehaviour
{
    public GameObject[] Togglables;

    private TextMeshPro tip;

    private bool canToggle;

    public void Start()
    {
        tip = GetComponent<TextMeshPro>();
        canToggle = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tip.enabled = true;
            canToggle = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tip.enabled = false;
            canToggle = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canToggle)
        {
            foreach (GameObject togglable in Togglables)
            {
                togglable.SetActive(!togglable.activeSelf);
            }
            this.gameObject.SetActive(false);
        }
    }




}
