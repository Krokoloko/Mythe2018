using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : Enemy {

    public float walkSpeed;
    public bool left;
    public float viewDistance;
    public float deathYPos;
    public bool spooked;

    private EnemyAnimationController _animator;
    private Vector3 _orgPos;
    private bool _suprised = false;
    private bool _grounded;
    private float _lagTime, _time, _dir;
    private Ray _scoutRay;

    void Start()
    {
        base.spriteRend = GetComponent<SpriteRenderer>();
        base.rb = GetComponent<Rigidbody>();
        spooked = false;
        _animator = new EnemyAnimationController("idle",gameObject);
        _orgPos = transform.position;
    }

    void Update()
    {
        _dir = left ? -1 : 1;
        base.spriteRend.flipX = !left;


        Vector3 dirRay = new Vector3(_dir, 0, 0);
        _scoutRay = new Ray(transform.position, dirRay);
        //Debug.Log("EnemyState = " + base.State);
        //Debug.Log("Grounded = " + _grounded);
        Debug.DrawRay(_scoutRay.origin, _scoutRay.direction, Color.yellow);
        RoutineAction();
        RoutineSwitch();

        if (OnDeathLocation())
        {
            Destroy(gameObject);
        }
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
        if(base.AirState == EnemyAirState.midair && base.rb.velocity.y <= 0.01f)
        {
            AirState = EnemyAirState.falling;
        }
        if(base.AirState == EnemyAirState.landing)
        {
            base.AirState = EnemyAirState.none;
        }

        switch (base.State)
        {
            case EnemyState.none:
                _animator.AnimatorSwitchTo("idle");
                State = EnemyState.idle;
                break;
            case EnemyState.idle:
                RaycastHit _hitIdl;
                Debug.DrawLine(_scoutRay.origin, _scoutRay.direction*viewDistance);
                if (Physics.Raycast(_scoutRay, out _hitIdl))
                {
                    if (_hitIdl.collider.gameObject.tag == "Player" && _hitIdl.distance < viewDistance)
                    {
                        _animator.AnimatorSwitchTo("spooked");
                        base.rb.AddForce(Vector3.up * 2,ForceMode.VelocityChange);
                        base.State = EnemyState.moving;
                        spooked = true;
                    }
                }
                break;
            case EnemyState.moving:
                RaycastHit _hitMov;
                if (Physics.Raycast(_scoutRay, out _hitMov))
                {
                    //Debug.Log("dist = " + _hitMov.distance + "  tag = " + _hitMov.collider.tag);
                    if (_grounded)
                    {
                        _animator.AnimatorSwitchTo("moving");
                        if (_hitMov.collider.gameObject.tag != "Player" || _hitMov.distance > viewDistance)
                        {
                            _animator.AnimatorSwitchTo("idle");
                            base.State = EnemyState.idle;
                        }
                    }
                }
                else
                {
                    base.State = EnemyState.idle;
                }
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
                _animator.AnimatorSwitchTo("idle");
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
            if (hit.distance <= (GetComponent<SpriteRenderer>().sprite.rect.height/2))
            {
                //if (hit.collider.gameObject.layer == 0 || hit.collider.gameObject.layer == 9)
                //{
                return true;
                //}
            }
        }
        return false;
    }
    
    private bool OnDeathLocation()
    {
        //Debug.Log(transform.position.y - _orgPos.y);
        if (transform.position.y < _orgPos.y - Mathf.Abs(deathYPos))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
