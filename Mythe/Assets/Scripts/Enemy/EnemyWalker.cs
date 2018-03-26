using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : Enemy {

    public float walkSpeed;
    public GameObject[] location = new GameObject[2];
    public bool left;
    public float viewDistance;

    private bool _suprised = false;
    private bool _grounded;
    private float _lagTime, _time, _dir;
    private Ray _scoutRay;

    void Start()
    {
        base.rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _dir = left ? -1 : 1;

        Vector3 dirRay = new Vector3(_dir, 0, 0);
        _scoutRay = new Ray(transform.position, dirRay);
        Debug.Log("EnemyState = " + base.State);
        Debug.Log("Grounded = " + _grounded);
        Debug.DrawRay(_scoutRay.origin, _scoutRay.direction, Color.yellow);
        RoutineAction();
        RoutineSwitch();
    }
    void RoutineSwitch()
    {
        switch (base.State)
        {
            case enemyState.idle:
                RaycastHit _hitIdl;
                if (Physics.Raycast(_scoutRay, out _hitIdl))
                {
                    if (_hitIdl.collider.gameObject.tag == "Player" && _hitIdl.distance < viewDistance)
                    {
//                        Debug.Log("Alarm");
                        base.rb.AddForce(Vector3.up * 2,ForceMode.VelocityChange);
                        base.State = enemyState.moving;
                    }
                }
                break;
            case enemyState.moving:
                RaycastHit _hitMov;
                if (Physics.Raycast(_scoutRay, out _hitMov))
                {
                    Debug.Log("dist = " + _hitMov.distance + "  tag = " + _hitMov.collider.tag);
                    if (_grounded)
                    {
                        Debug.Log("test");
//                        if (_hitMov.distance > viewDistance) Debug.Log("overdistance on moving state");
//                        if (_hitMov.collider.gameObject.tag != "Player") Debug.Log("can't see player.");
                        if (_hitMov.collider.gameObject.tag != "Player" || _hitMov.distance > viewDistance)
                        {
                            Debug.Log("test succesfull");
                            base.State = enemyState.idle;
                        }
                    }
                }
                else
                {
                    base.State = enemyState.idle;
                }
                break;
            case enemyState.none:
                State = enemyState.idle;
                break;
        }
    }

    private void RoutineAction()
    {
        switch (base.State)
        {
            case enemyState.moving:
                if (_grounded)
                {
                    base.rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    Vector3 direction = new Vector3(_dir, 0, 0);
                    Vector3 velocity = direction * walkSpeed * Time.deltaTime;
                    base.rb.MovePosition(base.rb.position += velocity);
                }
                break;
            case enemyState.idle:
                base.rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                break;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.position.y <= transform.position.y)
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.position.y <= transform.position.y)
        {
            _grounded = false;
        }
    }
    /*
    private bool OnLocation()
    {
        if (transform.position == location[0].transform.position || transform.position == location[1].transform.position)
        {
            _left = !_left;
            return true;
        }
        else
        {
            return false;
        }
    }*/
}
