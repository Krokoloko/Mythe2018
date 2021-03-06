﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour {


    public enum ClimbState {none, climbingNeutral, climbingUp, climbingDown};
    public ClimbState state = ClimbState.none;
    public float climbUpSpeed, climbDownSpeed, horSpeed; 

    
    private GameObject _player;
    private Walk _walk;
    private Jump _jump;
    private Drag _drag;
    private Crouch _crouch;
    private PlayerAnimationController _animator;
    private Rigidbody _rb;

    private bool _climbing;

    private GameObject[] _ladders;

	void Start ()
    {

        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = _player.GetComponent<Rigidbody>();
        _walk = _player.GetComponent<Walk>();
        _jump = _player.GetComponent<Jump>();
        _drag = _player.GetComponent<Drag>();
        _crouch = _player.GetComponent<Crouch>();
        _animator = _player.GetComponent<PlayerAnimationController>();
    }

    void Update()
    {
        _ladders = GameObject.FindGameObjectsWithTag("ladder");
        if (_ladders.Length >= 0)
        {
            RoutineSwitch();
            ClimbRoutine();
        }
        SwitchScripts();
    }

    private void RoutineSwitch()
    {
        switch (state)
        {
            case ClimbState.none:

                if (InRange() && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && _jump.state != Jump.jumpState.falling)
                {
                    state = ClimbState.climbingNeutral;
                    _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                }
                break;

            case ClimbState.climbingDown:

                if (!InRange())
                {
                    state = ClimbState.none;
                    _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionZ;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        state = ClimbState.climbingNeutral;
                        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                    }
                }
                else
                {
                    state = ClimbState.climbingNeutral;
                    _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                }
                break;

            case ClimbState.climbingUp:

                if (!InRange())
                {
                    state = ClimbState.none;
                    _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    if (Input.GetKey(KeyCode.S))
                    {
                        state = ClimbState.climbingNeutral;
                        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                    }
                }
                else
                {
                    state = ClimbState.climbingNeutral;
                    _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                }
                break;

            case ClimbState.climbingNeutral:

                
                if (!InRange())
                {
                    state = ClimbState.none;
                    _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                }
                if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
                {
                    break;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    state = ClimbState.climbingUp;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    state = ClimbState.climbingDown;
                }
                
                break;
        }
    }

    private void ClimbRoutine()
    {
        switch (state)
        {
            case ClimbState.none:
                _climbing = false;
                break;

            case ClimbState.climbingDown:
                _animator.AnimatorSwitchTo("climbingDown");
                _climbing = true;
                _rb.MovePosition(_rb.transform.position + Vector3.down * climbDownSpeed);
                
                break;

            case ClimbState.climbingUp:
                _animator.AnimatorSwitchTo("climbingUp");
                _climbing = true;
                _rb.MovePosition(_rb.transform.position + Vector3.up * climbUpSpeed);
                break;

            case ClimbState.climbingNeutral:
                _animator.AnimatorSwitchTo("climbing");
                _climbing = true;
                break;
        }
    }

    ///turns some scripts and the rigidbody gravity on and off for mechanic purposes
    private void SwitchScripts()
    {
        if (_climbing)
        {
            _rb.useGravity = false;
            _jump.enabled = false;
            _drag.enabled = false;
            _crouch.enabled = false;
            Debug.Log("Its climbing");
        }
        else
        {
            _rb.useGravity = true;
            _jump.enabled = true;
            _drag.enabled = true;
            _crouch.enabled = true;
        }
    }

    private void HorizontalClimbing()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (!Input.GetKey(KeyCode.A))
            {
                _rb.MovePosition(Vector3.right * horSpeed);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (!Input.GetKey(KeyCode.D))
            {
                _rb.MovePosition(Vector3.left * horSpeed);
            }
        }
    }

    private bool InRange()
    {
        for (int i = 0; i < _ladders.Length; i++)
        {
            if (_ladders[i].GetComponent<Ladder>().active)
            {
                return true;
            }
        }
        return false;
    } 
}
