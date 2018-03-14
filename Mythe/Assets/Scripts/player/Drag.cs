using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    //  var hit : RaycastHit
    public float pushforce;
    public float pullforce = 1;
    private GameObject player;
    private Walk _playerWalk;
    private Rigidbody _rb;
    private float _normSpeed; 


    void Start()
    {
        _playerWalk = GetComponent<Walk>();
        _rb = GetComponent<Rigidbody>();
        _normSpeed = _playerWalk.moveSpeed;
    }

void Update()
    {


        Debug.Log(_playerWalk.moveSpeed);


    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "MoveAble")
        {
            Debug.Log("test1");

            if (Input.GetKeyDown(KeyCode.L))
            {
                other.rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                _playerWalk.moveSpeed *= (pushforce /10);

                /*float distanceWanted = 3.0f;

                Vector3 diff = transform.position - player.transform.position;
                diff.y = 0.0f; // ignore Y (as requested in question)
                transform.position = player.transform.position + diff.normalized * distanceWanted;*/
            }
            if (Input.GetKeyUp(KeyCode.L))
            {
                _playerWalk.moveSpeed =_normSpeed;
                other.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            }
        }
    }
    void OnCollisionExit(Collision other)
    {
        _playerWalk.moveSpeed = _normSpeed;
        other.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
    }
}

