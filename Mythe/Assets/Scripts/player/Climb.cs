using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour {


    public enum ClimbState {none, climbingNeutral, climbingUp, climbingDown};
    public ClimbState state = ClimbState.none;
    private GameObject _player;
    private Walk _walk;
    private Jump _jump;
    private Drag _drag;
    private Crouch _crouch;

    private bool _climbing;

    private GameObject[] _ladders;

	void Start ()
    {

        _player = GameObject.FindGameObjectWithTag("Player");
        _walk = _player.GetComponent<Walk>();
        _jump = _player.GetComponent<Jump>();
        _drag = _player.GetComponent<Drag>();
        _crouch = _player.GetComponent<Crouch>();
        _ladders = GameObject.FindGameObjectsWithTag("ladder");

	}

    void Update()
    {
        RoutineSwitch();
    }

    private void RoutineSwitch()
    {
        switch (state)
        {
            case ClimbState.none:

                if (InRange() && Input.GetKey(KeyCode.W))
                {
                    state = ClimbState.climbingNeutral;
                }
                break;

            case ClimbState.climbingDown:

                if (!InRange())
                {
                    state = ClimbState.none;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        state = ClimbState.climbingNeutral;
                    }
                }
                else
                {
                    state = ClimbState.none;
                }
                break;

            case ClimbState.climbingUp:

                if (!InRange())
                {
                    state = ClimbState.none;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    if (Input.GetKey(KeyCode.S))
                    {
                        state = ClimbState.climbingNeutral;
                    }
                }
                else
                {
                    state = ClimbState.none;
                }
                break;

            case ClimbState.climbingNeutral:

                if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
                {
                    break;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    state = ClimbState.climbingUp;
                }
                if (Input.GetKey(KeyCode.S))
                {
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
                _climbing = true;
                break;

            case ClimbState.climbingUp:
                _climbing = true;
                break;

            case ClimbState.climbingNeutral:
                _climbing = true;
                break;
        }
    }

    private void SwitchScripts()
    {
        if (_climbing)
        {
            _walk.enabled = !_climbing;
            _jump.enabled = !_climbing;
            _drag.enabled = !_climbing;
            _crouch.enabled = !_climbing;
        }
        else
        {
            _walk.enabled = _climbing;
            _jump.enabled = _climbing;
            _drag.enabled = _climbing;
            _crouch.enabled = _climbing;
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
