using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static string[] sceneNames = new string[] { "Level1", "SafeArea", "Level-something" };

    public static IEnumerator ChangeScene(string newSceneName, string oldSceneName, Action postLoad)
    {
        if (IsSceneNameValid(newSceneName, oldSceneName))
        {
            
            AsyncOperation loading = SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);
            yield return loading;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newSceneName));
        
        postLoad();

        if (!string.IsNullOrEmpty(newSceneName))
        {
            Debug.Log(newSceneName);
            yield return new WaitForEndOfFrame();
            AsyncOperation unloading = SceneManager.UnloadSceneAsync(oldSceneName);
            yield return unloading;
        }
    }

    public static bool IsSceneNameValid(string newSceneName, string oldSceneName)
    {
        Debug.Log(sceneNames.Contains(newSceneName));
        return sceneNames.Contains(newSceneName) && sceneNames.Contains(oldSceneName);
    }
}
