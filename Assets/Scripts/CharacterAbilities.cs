using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAbilities : MonoBehaviour
{
    public bool TypeOneAvailable = false;
    public bool TypeTwoAvailable = false;
    public bool TypeThreeAvailable = false;

    public Button TypeOneButton;
    public Button TypeTwoButton;

    public PlayerController2D PlayerPrefab;

    private void Awake()
    {
    }

    public void SetType(int type)
    {
        if (type > 0 && type <= 3)
        {
            if (type == 1)
            {
                TypeOneAvailable = true;
            }
            else if (type == 2)
            {
                TypeTwoAvailable = true;
                TypeOneButton.gameObject.SetActive(true);
                TypeTwoButton.gameObject.SetActive(true);
            }
            else
            {
                TypeThreeAvailable = true;
            }
        }
    }

    public void ChangeType(int type)
    {
        if (type == 1)
        {
            Debug.Log("1");
            PlayerPrefab.jumpForce = 35.0f;
            PlayerPrefab.SetColor(new Color(255f / 255.0f, 255f / 255.0f, 255f / 255.0f, 1f));
        } else if (type == 2)
        {
            Debug.Log("2");
            PlayerPrefab.jumpForce = 50.0f;
            PlayerPrefab.SetColor(new Color(233f / 255.0f, 91f / 255.0f, 91f / 255.0f, 1f));
        }
    }
}
