using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBossController : MonoBehaviour
{

    private ISpeedBossState currentState;
    
    public Transform GroundLeftConstraint;
    public Transform GroundRightConstraint;

    public Transform PlatformLeftConstraint;
    public Transform PlatformRightConstraint;

    public Transform PlatformLeftLeapConstraint;
    public Transform PlatformRightLeapConstraint;

    public Transform MiddleConstraint;

    public FollowCurve CurveFollower; 

    private Vector3 _lastPosition;
    private bool _lookingRight = true;

    private SpriteRenderer spriteRenderer;
    public RocketSpeedBoss rocketPrefab;

    public Image bossHealthImage;
    private int _maxHealth;
    private int _health;

    public bool bossAlive;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeState(new SpeedBossIdleState());
        CurveFollower = GetComponent<FollowCurve>();
        _lastPosition = transform.position;
        _maxHealth = 60;
        _health = _maxHealth;
        bossAlive = true;
    }

    private void Update()
    {
        // Changes Sprite direction based on X movement
        if(_lookingRight && (transform.position.x - _lastPosition.x) < 0f)
        {
            spriteRenderer.flipX = true;
            _lookingRight = false;
        }
        else if(!_lookingRight && (transform.position.x - _lastPosition.x) > 0f)
        {
            spriteRenderer.flipX = false;
            _lookingRight = true;
        }
        

        // Call State Update
        currentState.Update();
        

        // Sets position for direction changing
        _lastPosition = transform.position;
    }

    /**
     * Changes States for combat
     * */
    public void ChangeState(ISpeedBossState newState)
    {

        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        Debug.Log("Entering state : " + currentState.ToString());
        currentState.Enter(this);
    }

    /**
     * Method to start jump curves
     * param[in] direction - True for right, False for left
     * param[in] heading - Decides curve heading (up, down, leap over)
     * */
    public void StartCurve(bool direction, Headings heading)
    {
        CurveFollower.StartCurve(direction, heading);
    }

    public void TakeDamage(int value)
    {
        _health -= value;
        bossHealthImage.fillAmount = (float)_health / _maxHealth;
        if (_health <= 0)
        {
            BossDeath();
        }
    }

    private void BossDeath()
    {
        ChangeState(new SpeedBossIdleState());
        transform.eulerAngles = new Vector3(0, 0, 90);
        bossAlive = false;
    }
    
    public void StartFight()
    {
        ChangeState(new SpeedBossActiveState());
    }
}
