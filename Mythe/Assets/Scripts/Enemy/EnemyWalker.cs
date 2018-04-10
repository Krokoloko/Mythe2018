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
        if (base.AirState == EnemyAirState.falling && IsGrounded())
        {
            base.AirState = EnemyAirState.landing;
        }
        if (base.rb.velocity.y <= 0.01f)
        {
            AirState = EnemyAirState.falling;
        }
        if (base.rb.velocity.y >= 0.01f)
        {
            AirState = EnemyAirState.midair;
        }

        switch (base.State)
        {
            case EnemyState.idle:
                RaycastHit _hitIdl;
                if (Physics.Raycast(_scoutRay, out _hitIdl))
                {
                    if (_hitIdl.collider.gameObject.tag == "Player" && _hitIdl.distance < viewDistance)
                    {
                        base.rb.AddForce(Vector3.up * 2,ForceMode.VelocityChange);
                        base.State = EnemyState.moving;
                    }
                }
                break;
            case EnemyState.moving:
                RaycastHit _hitMov;
                if (Physics.Raycast(_scoutRay, out _hitMov))
                {
                    Debug.Log("dist = " + _hitMov.distance + "  tag = " + _hitMov.collider.tag);
                    if (_grounded)
                    {
                        Debug.Log("test");
                        if (_hitMov.collider.gameObject.tag != "Player" || _hitMov.distance > viewDistance)
                        {
                            Debug.Log("test succesfull");
                            base.State = EnemyState.idle;
                        }
                    }
                }
                else
                {
                    base.State = EnemyState.idle;
                }
                break;
            case EnemyState.none:
                State = EnemyState.idle;
                break;
        }
    }

    private void RoutineAction()
    {
        switch (base.State)
        {
            case EnemyState.moving:
                if (IsGrounded() || _grounded)
                {
                    base.rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    Vector3 direction = new Vector3(_dir, 0, 0);
                    Vector3 velocity = direction * walkSpeed * Time.deltaTime;
                    base.rb.MovePosition(base.rb.position += velocity);
                }
                break;
            case EnemyState.idle:
                base.rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                break;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.position.y <= transform.position.y)
        {
            _grounded = true;
            base.AirState = EnemyAirState.none;
        }
        else
        {
            base.AirState = EnemyAirState.midair;
            _grounded = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.position.y <= transform.position.y)
        {
            base.AirState = EnemyAirState.midair;
            _grounded = false;
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down);
        Debug.DrawRay(ray.origin, ray.direction, Color.black);


        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance <= (GetComponent<SpriteRenderer>().sprite.rect.height / 2))
            {
                //if (hit.collider.gameObject.layer == 0 || hit.collider.gameObject.layer == 9)
                //{
                return true;
                //}
            }
        }
        return false;
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
