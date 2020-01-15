using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLimb : MonoBehaviour
{
    public GameObject LimbSolver;
    public GameObject Target;

    public float AttackCooldown;
    private float _timer = 0.0f;

    private bool _targetInRange;
    private bool _attacking;
    private bool _returning;
    private Vector3 _attackPosition;
    private Vector3 _defeaultPosition;

    void Start()
    {
        _targetInRange = false;
        _attacking = false;
        _returning = false;
        _defeaultPosition = LimbSolver.transform.position;
    }

    void Update()
    {
        //Debug.Log(_timer);
        if (_targetInRange && _timer > AttackCooldown && !_attacking)
        {
            Debug.Log("Setting attack position");
            _attackPosition = Target.transform.position;
            _attacking = true;
            _timer = 0;
        }

        if (_attacking && !_returning)
        {
            LimbSolver.transform.position = Vector3.MoveTowards(LimbSolver.transform.position, _attackPosition, 20f * Time.deltaTime);
        }

        if (_returning)
        {
            LimbSolver.transform.position = Vector3.MoveTowards(LimbSolver.transform.position, _defeaultPosition, 10f * Time.deltaTime);
        }

        if (_attacking && LimbSolver.transform.position == _attackPosition)
        {
            Debug.Log("Finished attack!");
            _returning = true;
        }

        if (_returning && LimbSolver.transform.position == _defeaultPosition)
        {
            Debug.Log("Finished return!");
            _returning = false;
            _attacking = false;
        }

        if (_timer < AttackCooldown)
        {
            _timer += Time.deltaTime;
        }

        Debug.DrawRay(LimbSolver.transform.position, Target.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Target.tag))
        {
            _targetInRange = true;
            Debug.Log(_targetInRange);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Target.tag))
        {
            _targetInRange = false;
            Debug.Log(_targetInRange);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Target.tag))
        {
            //StartCoroutine(Hit());
        }
    }

    IEnumerator Hit()
    { 
        yield return new WaitForSeconds(1);
        LimbSolver.transform.position = Vector3.MoveTowards(LimbSolver.transform.position, Target.transform.position, 10f * Time.deltaTime);
    }
}
