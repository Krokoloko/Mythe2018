using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    //  var hit : RaycastHit
    public float pushforce = 1;
    public float pullforce = 1;
    private GameObject player;
    private Walk _playerWalk;
    private Rigidbody _rb;
    private float _normSpeed;
    private bool Dragging = false;
    private GameObject target;
    private MeshRenderer _mesh;
    Vector3 targetposition;
    float diffX;
    [SerializeField]
    private float throwforce = 5;


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
            float test = (target.transform.position + (_mesh.bounds.size / 2) - gameObject.transform.position).sqrMagnitude;
            Debug.Log("range condition " + test + " radius condition " + _mesh.bounds.size.x * _mesh.bounds.size.x);

            print("drag");
            targetposition = new Vector3(transform.position.x + diffX, target.transform.position.y, transform.position.z);
            target.GetComponent<Rigidbody>().MovePosition(targetposition);
            if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.LeftShift) || (target.tag == "MoveAble" && (target.transform.position - gameObject.transform.position).sqrMagnitude > _mesh.bounds.size.x * _mesh.bounds.size.x))
            {
                print("stop");
                Dragging = false;
                if (target.tag == "ThrowObject")
                {
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
<<<<<<< HEAD
//<<<<<<< HEAD
//<<<<<<< HEAD
//=======
//>>>>>>> dbabfc2bbd0b0e56bf0a34741f816c936fc7b644
=======
>>>>>>> 2f077e177df42f9cf3f544e6b882da8fbc1cbd8b
                target = other.gameObject;
                _mesh = target.GetComponent<MeshRenderer>();
                diffX = target.transform.position.x - transform.position.x;
                target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                _playerWalk.moveSpeed = pushforce;
<<<<<<< HEAD
//<<<<<<< HEAD
//=======
                diffX = target.transform.position.x + 0.08f - transform.position.x;
                target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                //_playerWalk.moveSpeed *= (pushforce / 2);
            }
        }
    }
//>>>>>>> f229eed1db2f1e148cca374cff175eb6b1ed5c87

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "MoveAble" || other.gameObject.tag == "ThrowObject")
        {
            target = other.gameObject;

      //      if (target.transform.position.x + 0.08f - transform.position.x)
            {
                Dragging = false;
//=======

//>>>>>>> dbabfc2bbd0b0e56bf0a34741f816c936fc7b644
=======

>>>>>>> 2f077e177df42f9cf3f544e6b882da8fbc1cbd8b
            }
        }
    }
}