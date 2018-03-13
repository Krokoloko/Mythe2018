using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public enum jumpState {none, falling, rising, midair};
    public float maxHeight;
    public float ySpeed;

    private jumpState _state = jumpState.none;
    private Rigidbody _rigidbody;
    private float originalHeight;
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        JumpRoutine();
	}
    private void JumpRoutine()
    {
        switch (_state)
        {
            case jumpState.none:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    originalHeight = transform.position.y;
                    _state = jumpState.rising;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    //print("pressing");
                    gameObject.transform.Translate(Vector3.up * ySpeed * Time.deltaTime);
                }
                break;
            case jumpState.rising:
                if (Input.GetKey(KeyCode.Space))
                {
                    //print("pressing");
                    gameObject.transform.Translate(Vector3.up * ySpeed * Time.deltaTime);
                }
                if (Input.GetKeyUp(KeyCode.Space) || (originalHeight + maxHeight) <= transform.position.x)
                {
                    //print("released");
                    _state = jumpState.midair;
                }
                break;
            case jumpState.midair:
                if (_rigidbody.velocity.y == 0)
                {
                    _state = jumpState.falling;
                }
                _rigidbody.AddForce(Vector3.down * Time.deltaTime * 0.5f);
                break;
            case jumpState.falling:
                Ray ray = new Ray(transform.position,Vector3.down);
                if (false)
                {
                    _state = jumpState.none;
                }
                break;
        }                
    }
}
