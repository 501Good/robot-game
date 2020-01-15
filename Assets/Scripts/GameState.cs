using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public List<LevelData> levels;
    public string currentLevelName;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentLevelName = SceneManager.GetActiveScene().name;
        //Debug.Log(SceneManager.GetActiveScene().GetRootGameObjects());
    }

    void GetEnemiesInScene(Scene scene)
    {
        GameObject.FindGameObjectsWithTag("Enemy");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (!levels.Exists(x => (x.levelName == scene.name)))
        {
            List<EnemyData> enemies = new List<EnemyData>();
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemies.Add(new EnemyData(enemy, false));
            }

            List<GameObject> checkpoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Checkpoint"));

            levels.Add(new LevelData(scene.name, enemies, checkpoints));
        }
    }

    public LevelData GetCurrentLevelData()
    {
        return levels.Find(x => (x.levelName == currentLevelName));
    }

}
