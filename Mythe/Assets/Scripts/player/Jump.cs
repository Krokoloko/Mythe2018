using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public enum jumpState {none, falling, rising };
    private jumpState state = jumpState.none;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //print("pressing");
            PlayerPhysics_.velocity = new Vector2(PlayerPhysics_.velocity.x, ySpeed * _Speed_);
            state = jumpState.rising;

        }
        if (Input.GetKeyUp(KeyCode.Space) || (originalPos + maxHeight) <= transform.position.x)
        {
            //print("released");
            state = jumpState.falling;
        }

    }
}
