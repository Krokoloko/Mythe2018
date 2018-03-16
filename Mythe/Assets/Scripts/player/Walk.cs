using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {

    private Rigidbody _rigidbody;
    public enum walkState {none, walking};

    public walkState state;
    public float moveSpeed;

    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            state = walkState.walking;
        }else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            state = walkState.walking;
        }
        else
        {
            state = walkState.none;
        }
	}
}
