using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public List<EnemyData> enemies;
    public List<GameObject> checkpoints;

    public LevelData(string levelName, List<EnemyData> enemies, List<GameObject> checkpoints)
    {
        this.levelName = levelName;
        this.enemies = enemies;
        this.checkpoints = checkpoints;
    }

    public bool IsLevel(string name)
    {
        return levelName.Equals(name);
    }

    public void SetEnemyIsDeadState(GameObject enemy, bool isDead)
    {
        enemies.Find(x => (x.enemyObject == enemy)).isDead = isDead;
    }
}
