using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public Button button;

    private void Awake()
    {
        button.onClick.AddListener(ClickedButton);
    }

    private void ClickedButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
