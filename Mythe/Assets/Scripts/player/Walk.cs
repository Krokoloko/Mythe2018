using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {

    private Rigidbody _rigidbody;
    public enum walkState {none, walking};

    public walkState state;
    public float moveSpeed;

    private float xInput;

    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}

    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate ()
    {
        if (xInput != 0)
        {
            Vector3 direction = new Vector3(xInput, 0, 0);
            Vector3 velocity = direction * moveSpeed * Time.deltaTime;
            _rigidbody.MovePosition(_rigidbody.position + velocity);
            // transform.Translate(direction * moveSpeed * Time.deltaTime);
            state = walkState.walking;
        }
        /*
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            state = walkState.walking;
        }else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            state = walkState.walking;
        }
        */
        else
        {
            state = walkState.none;
        }
	}
}
