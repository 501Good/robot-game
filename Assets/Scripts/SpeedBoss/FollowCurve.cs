using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Headings{
    jumpUp, jumpDown, leapOver
}

public class FollowCurve : MonoBehaviour
{
    public AnimationCurve xCurve, yCurve;
    public AnimationCurve leapXCurve, leapYCurve, leapDownYCurve;

    private AnimationCurve _chosenXCurve, _chosenYCurve;
    private int _xMultiplier, _yMultiplier, _timeMultiplier;
    private Rigidbody2D rb;
    private int curvePosition = 0;
    public float timeElapsed = 0;
    private bool started = false;

    private Vector2 startPosition;

    public bool JumpDirectionRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!started)
        {
            timeElapsed = 0;
            startPosition = transform.position;
        }
        else
        {
            timeElapsed += Time.deltaTime * _timeMultiplier;

            int direction;
            if (JumpDirectionRight)
                direction = 1;
            else
                direction = -1;

            rb.MovePosition(new Vector2(
                startPosition.x + _chosenXCurve.Evaluate(timeElapsed) * _xMultiplier * direction,
                startPosition.y + _chosenYCurve.Evaluate(timeElapsed) * _yMultiplier));
            
            if(timeElapsed > 1)
            {
                this.started = false;
            }
        }
    }

    public void StartCurve(bool direction, Headings where)
    {
        _timeMultiplier = 1;
        switch (where)
        {

            case Headings.jumpUp: // Jumping up
                _chosenXCurve =leapXCurve;
                _chosenYCurve = leapYCurve;
                _xMultiplier = 3;
                _yMultiplier = 7;
                _timeMultiplier = 2;
                break;
            case Headings.jumpDown: // Jumping down
                _chosenXCurve = leapXCurve;
                _chosenYCurve = leapDownYCurve;
                _xMultiplier = 3;
                _yMultiplier = 7;
                _timeMultiplier = 2;
                break;
            case Headings.leapOver: // Leaping over
                _chosenXCurve = xCurve;
                _chosenYCurve = yCurve;
                _xMultiplier = 16;
                _yMultiplier = 2;
                break;
            default:
                Debug.LogError("FollowCurve default startcurve error message");
                break;
        }
        JumpDirectionRight = direction;
        started = true;
    }
}
