using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public enum jumpState {none, falling, rising, midair, landing};
    public jumpState state = jumpState.none;

    public float maxHeight, jumpMultiplier, maxJumpSpeed, maxFallSpeed;
    public float landingLag = 0.5f;
    public bool canJump = true;

    private Rigidbody _rigidbody;
    private float _originalHeight,_landTime, _jumpHold;
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        Debug.Log(state);
        JumpRoutine();
        RoutineSwitch();
    }

    private void JumpRoutine()
    {
        switch (state)
        {
            case jumpState.none:
                if (Input.GetKeyDown(KeyCode.Space) && canJump)
                {
                    //_rigidbody.MovePosition(_rigidbody.position += (Vector3.up*jumpMultiplier*Time.deltaTime));
                    _rigidbody.AddForce(new Vector3(0, 1) * jumpMultiplier*1.5f * Time.deltaTime,ForceMode.Impulse);
                    //_rigidbody.velocity += new Vector3(0, 1) * (jumpMultiplier*15) * Time.deltaTime;
                }
                break;
            case jumpState.rising:
                
                if (_rigidbody.velocity.y >= maxJumpSpeed)
                {
                    _rigidbody.AddForce(new Vector3(0, 1) * jumpMultiplier / 3 * Time.deltaTime, ForceMode.VelocityChange);
                    //_rigidbody.velocity += new Vector3(0,1) * jumpMultiplier/2 * Time.deltaTime;
                }else if (Input.GetKey(KeyCode.Space))
                {
                    _rigidbody.AddForce(new Vector3(0, 1) * jumpMultiplier * Time.deltaTime, ForceMode.VelocityChange);
                    //_rigidbody.velocity += new Vector3(0, 1) * jumpMultiplier * Time.deltaTime;
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
        
        switch (state)
        {
            case jumpState.rising:
                if (Input.GetKeyUp(KeyCode.Space) || (_originalHeight + maxHeight) <= transform.position.y)
                {
                    state = jumpState.midair;
                }
                break;
            case jumpState.landing:
                if (Time.time - _landTime >= landingLag)
                {
                    state = jumpState.none;
                    _landTime = 0;
                }
                break;
            case jumpState.midair:
                if (_rigidbody.velocity.y < 0)
                {
                    state = jumpState.falling;
                }
                break;
            case jumpState.falling:
                if (IsGrounded())
                {
                    state = jumpState.landing;
                    _landTime = Time.time;
                }
                break;
            case jumpState.none:
                if (Input.GetKey(KeyCode.Space) && canJump)
                {
                    _originalHeight = transform.position.y;
                    state = jumpState.rising;
                }
                if (_rigidbody.velocity.y < -0.01)
                {
                    state = jumpState.falling;
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
}
