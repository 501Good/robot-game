using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum enemyType
{
    CargoBot, Bat, Worm, NaN
}
public class EnemyController : MonoBehaviour
{

    
    public enemyType CurrentEnemyType;
    private IEnemyState currentState;

    // Movement
    public LayerMask groundMask;
    public float speed;
    public bool lookingRight = false;
    public GameObject BatRockPrefab;

    private GameState gameState;

    private void Awake()
    {
        gameState = GameObject.FindObjectOfType<GameState>();

        switch (CurrentEnemyType)
        {
            case enemyType.Worm:
                ChangeState(new EnemyWormPatrolState());
                break;
            case enemyType.Bat:
                ChangeState(new EnemyBatPatrolState());
                break;
            case enemyType.CargoBot:
                ChangeState(new EnemyLoaderPatrolState());
                break;
            default:
                Debug.LogError("This EnemyController state has not been implemented: " + CurrentEnemyType);
                break;
        }
    }
    

    void Update()
    {
        currentState.Update();
    }

    public void ChangeState(IEnemyState newState)
    {

        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter(this);
    }

    private void Death()
    {
        //gameState.GetCurrentLevelData().SetEnemyIsDeadState(this.gameObject, true);
        Destroy(this.gameObject);
    }
}
