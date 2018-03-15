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
    private bool Dragging = false;
    private GameObject target;
    Vector3 targetposition;
    float diffX;


    void Start()
    {
        _playerWalk = GetComponent<Walk>();
        _rb = GetComponent<Rigidbody>();
        _normSpeed = _playerWalk.moveSpeed;
    }

void Update()
    {

        if (Dragging)
        {
            print("drag");

           
            //

            
            
            targetposition = new Vector3(transform.position.x + diffX,target.transform.position.y,transform.position.z);
            target.GetComponent<Rigidbody>().MovePosition(targetposition);
            if (Input.GetKeyUp(KeyCode.L))
            {
                print("stop");
                Dragging = false;
                _playerWalk.moveSpeed = _normSpeed;
                target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            }
        }
        
     


    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "MoveAble")
        {
            Debug.Log("im touching");

           

            if (Input.GetKeyDown(KeyCode.L))
            {
                Dragging = true;
                target = other.gameObject;
                diffX = target.transform.position.x - transform.position.x;
                target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                //_playerWalk.moveSpeed *= (pushforce / 2);

            }
            
        }
    }
}

