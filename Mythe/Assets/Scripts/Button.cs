using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public bool active = false;


void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "MoveAble" || col.gameObject.tag == "ThrowObject" || col.gameObject.tag == "Enemy")
        {
            active = true;
            print("nyess");
        }
    }
void OnCollisionExit()
    {
        active = false;
    }
}
