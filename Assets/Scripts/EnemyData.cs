using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public GameObject enemyObject;
    public bool isDead;

    public EnemyData(GameObject enemyObject, bool isDead)
    {
        this.enemyObject = enemyObject;
        this.isDead = isDead;
    }
}
