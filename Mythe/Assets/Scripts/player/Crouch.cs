using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour {

    public enum crouchState {none, crouch, crawl};

    public crouchState state = crouchState.none;

    private Walk _playerWalk;
    private Jump _playerJump;
    private BoxCollider _collider;
    private float _crawlColY, _colCenterY,_crawlSpeed, _originSpeed;
    private Vector3 _colSizeOrg, _colCenterOrg;

    public float crawlMultiplier;
    public float collisionMultiplier;
    public float crawlSpeedMultiplier;

	void Start () {
        _playerWalk = GetComponent<Walk>();
        _playerJump = GetComponent<Jump>();
        _collider = GetComponent<BoxCollider>();

        _colSizeOrg = new Vector3(_collider.size.x,_collider.size.y,_collider.size.z);
        _colCenterOrg = new Vector3(_collider.center.x, _collider.center.y, _collider.center.z);

        _originSpeed = _playerWalk.moveSpeed;

        _crawlSpeed = _originSpeed * crawlSpeedMultiplier;
        _crawlColY = _collider.size.y * collisionMultiplier;
        _colCenterY = -(_crawlColY*crawlMultiplier);
    }
	
	void Update () {
        Routine();
        RoutineSwitch();
	}

    private void Routine()
    {
        switch (state)
        {
            case crouchState.none:
                _playerWalk.moveSpeed = _originSpeed;
                _collider.size = new Vector3(_collider.size.x, _colSizeOrg.y, _collider.size.z);
                _collider.center = new Vector3(_collider.center.x, _colCenterOrg.y, _collider.center.z);

                break;
            case crouchState.crouch:
                _playerWalk.moveSpeed = _crawlSpeed;
                
                _collider.size = new Vector3(_collider.size.x, _crawlColY, _collider.size.z);
                _collider.center = new Vector3(_collider.center.x, _colCenterY, _collider.center.z);

                break;
            case crouchState.crawl:
                _playerWalk.moveSpeed = _crawlSpeed;
                break;

        }
    }
    private void RoutineSwitch()
    {

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))
        {
            if (_playerWalk.state == Walk.walkState.none && _playerJump.state == Jump.jumpState.none)
            {
                _playerJump.canJump = false;
                state = crouchState.crouch;
            }
        }
        if (Input.GetKeyUp(KeyCode.S) || !Input.GetKey(KeyCode.S) || _playerJump.state != Jump.jumpState.none)
        {
            _playerJump.canJump = true;
            state = crouchState.none;
        }
        if (state == crouchState.crouch && _playerWalk.state == Walk.walkState.walking)
        {
            state = crouchState.crawl;
        }
    }
}
