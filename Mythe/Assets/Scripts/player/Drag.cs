using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    //  var hit : RaycastHit
    private float pushforce = 1;
    private float pullforce = 1;
    private GameObject player;
    private Walk _playerWalk;
    private Rigidbody _rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _playerWalk = player.GetComponent<Walk>();
        _rb = GetComponent<Rigidbody>();

    }

void Update()
    {


        

    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "MoveAble")
        {
            Debug.Log("test1");

            if (Input.GetKeyDown(KeyCode.L))
            {
                _rb.constraints.
                /*float distanceWanted = 3.0f;

                Vector3 diff = transform.position - player.transform.position;
                diff.y = 0.0f; // ignore Y (as requested in question)
                transform.position = player.transform.position + diff.normalized * distanceWanted;*/
            }
        }
    }
}

