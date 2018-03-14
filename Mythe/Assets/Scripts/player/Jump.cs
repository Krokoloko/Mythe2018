using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public enum jumpState {none, falling, rising, midair, landing};
    public float maxHeight;
    public float jumpMultiplier;
    public float maxJumpSpeed;
    public float maxFallSpeed;
    public float landingLag = 0.5f;

    private jumpState _state = jumpState.none;
    private Rigidbody _rigidbody;
    private float _originalHeight;
    private float _landTime;
    private float _jumpHold;
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        Debug.Log(_state);
        JumpRoutine();
        RoutineSwitch();
        //Debug.Log(_rigidbody.velocity.y);
    }
    private void JumpRoutine()
    {
        switch (_state)
        {
            case jumpState.none:
                if (Input.GetKey(KeyCode.Space))
                {
                    _rigidbody.velocity.Set(_rigidbody.velocity.x, maxJumpSpeed, _rigidbody.velocity.z);
                }
                break;
            case jumpState.rising:
                if (Input.GetKey(KeyCode.Space))
                {
                    Debug.Log("pressing");
                    _rigidbody.velocity += new Vector3(0,1) * jumpMultiplier * Time.deltaTime;
                }
                if (_rigidbody.velocity.y >= maxJumpSpeed)
                {
                    _rigidbody.velocity.Set(_rigidbody.velocity.x, maxJumpSpeed, _rigidbody.velocity.z);
                }
                break;
            case jumpState.falling:
                if(_rigidbody.velocity.y > -maxFallSpeed)
                {
                    _rigidbody.velocity.Set(_rigidbody.velocity.x, -maxFallSpeed, _rigidbody.velocity.z);
                }
                break;
        }                
    }
    private void RoutineSwitch()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _state = jumpState.falling;
        }
        switch (_state)
        {
            case jumpState.rising:
                if (Input.GetKeyUp(KeyCode.Space) || (_originalHeight + maxHeight) <= transform.position.y)
                {
                    
                    _state = jumpState.falling;
                }
                break;
            case jumpState.landing:
                if (Time.time - _landTime >= landingLag)
                {
                    _state = jumpState.none;
                    _landTime = 0;
                }
                break;
            case jumpState.falling:
                if (IsGrounded())
                {
                    _state = jumpState.landing;
                    _landTime = Time.time;
                }
                break;
            case jumpState.none:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _originalHeight = transform.position.y;
                    _state = jumpState.rising;
                }
                break;
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
                Debug.Log("Landed");
                return true;
            }
        }
        return false;
    }
}
