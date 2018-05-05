﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : InteractableObject
{
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected override bool ReasonInteraction()
    {
        return (!Input.GetKeyUp(KeyCode.RightShift) || !Input.GetKeyUp(KeyCode.LeftShift));
    }

    protected override void InteractionAction()
    {
        float diffX = player.position.x - transform.position.x;
        Vector3 targetPosition = new Vector3(player.position.x - diffX, 0, 0);
        print(player.position);
        transform.position = targetPosition;
        //_rb.MovePosition(_rb.position + targetPosition);
    }


}

/*
    //  var hit : RaycastHit
    public float pushforce = 1;
    public float pullforce = 1;
   
   

    private float _normSpeed;
    
    private MeshRenderer _mesh;


    
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
            //float test = (target.transform.position + (_mesh.bounds.size / 2) - gameObject.transform.position).sqrMagnitude;
            //Debug.Log("range condition " + test + " radius condition " + _mesh.bounds.size.x * _mesh.bounds.size.x);

            print("drag");
            if (target.tag == "ThrowObject")
            {
                targetposition = new Vector3(transform.position.x + diffX, transform.position.y, transform.position.z);
            }
            else if (target.tag == "MoveAble")
            {
                targetposition = new Vector3(transform.position.x + diffX, target.transform.position.y, transform.position.z);
            }

            target.GetComponent<Rigidbody>().MovePosition(targetposition);
            if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.LeftShift) || (target.tag == "MoveAble" && (target.transform.position - gameObject.transform.position).sqrMagnitude > _mesh.bounds.size.x * _mesh.bounds.size.x))
            {
                print("stop");
                Dragging = false;
                if (target.tag == "ThrowObject")
                {
                    target.GetComponent<Rigidbody>().useGravity = true;
                    if (transform.position.x > target.transform.position.x)
                    {
                        Debug.Log("Player is colliding on target's right side");
                        target.GetComponent<Rigidbody>().AddForce(new Vector3(-0.4f, 1.1f, 0) * throwforce, ForceMode.Impulse);
                    }
                    else if (transform.position.x < target.transform.position.x)
                    {
                        Debug.Log("Player is colliding on target's left side");
                        target.GetComponent<Rigidbody>().AddForce(new Vector3(0.4f, 1.1f, 0) * throwforce, ForceMode.Impulse);
                    }

                }
                else
                {
                    _playerWalk.moveSpeed = _normSpeed;
                    target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
                }
            }
        }
        else
        {
            Debug.Log("Not dragging");
        }

    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "MoveAble" || other.gameObject.tag == "ThrowObject")
        {
            Debug.Log("im touching");
            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
            {

                Dragging = true;
                target = other.gameObject;
                _mesh = target.GetComponent<MeshRenderer>();
                diffX = target.transform.position.x - transform.position.x;
                target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                _playerWalk.moveSpeed = pushforce;
                if (other.gameObject.tag == "ThrowObject")
                {
                    target.GetComponent<Rigidbody>().useGravity = false;
                } 

            }
*/