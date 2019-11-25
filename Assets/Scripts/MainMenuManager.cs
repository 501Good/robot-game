using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(LoadSceneAfterDelay(0.5f));
    }

    private IEnumerator LoadSceneAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitAfterDelay(0.5f));
    }

    private IEnumerator QuitAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Application.Quit();
    }
}
