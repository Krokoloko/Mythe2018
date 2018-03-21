using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : Enemy {

    public float walkSpeed;
    public GameObject[] location = new GameObject[2];

    private float _lagTime;
    private float _time;
    private bool _left; 
    private Ray _scoutRay;
    private RaycastHit _hit;

    void Start()
    {
        _left = base.RandomBool();
        base.rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.DrawRay(_scoutRay.origin, _scoutRay.direction, Color.red);
        RoutineAction();
        RoutineSwitch();
    }
    void RoutineSwitch()
    {
        switch (base.State)
        {
            case enemyState.idle:
                if (Physics.Raycast(_scoutRay, out _hit))
                {
                    if (_hit.collider.gameObject.tag == "Player")
                    {
                        Debug.Log("Alarm");
                        base.rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                        base.State = enemyState.moving;
                    }
                }
                break;
            case enemyState.moving:
                if (Physics.Raycast(_scoutRay, out _hit))
                {
                    if (_hit.collider.gameObject.tag != "Player")
                    {
                        Debug.Log("gone");
                        base.rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        base.State = enemyState.idle;
                    }
                }
                break;
            case enemyState.none:
                base.rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                State = enemyState.idle;
                break;
        }
    }
    void RoutineAction()
    {
        float dir = _left ? -1 : 1;

        switch (base.State)
        {
            case enemyState.moving:
                Vector3 direction = new Vector3(dir,0,0);
                Vector3 velocity = direction * walkSpeed * Time.deltaTime;
                base.rb.MovePosition(base.rb.position += velocity);
                break;
            case enemyState.idle:
                Vector3 dirRay = new Vector3(dir,0,0);
                _scoutRay = new Ray(transform.position,dirRay);
                break;
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
