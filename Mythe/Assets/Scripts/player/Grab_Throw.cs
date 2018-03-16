using UnityEngine;
using System.Collections;

public class Grab_Throw : MonoBehaviour
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
            print("drag");


            //



            targetposition = new Vector3(transform.position.x + diffX, target.transform.position.y, transform.position.z);
            target.GetComponent<Rigidbody>().MovePosition(targetposition);
            if (Input.GetKeyUp(KeyCode.L))
            {
                print("stop");
                Dragging = false;
                if (target.tag == "throwObject")
                {
                    target.GetComponent<Rigidbody>().AddForce(new Vector3(1, 1, 0) * throwforce, ForceMode.Impulse);
                }
                else
                {
                    _playerWalk.moveSpeed = _normSpeed;
                    target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
                }
                
            }
        }




    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "MoveAble"|| other.gameObject.tag == "throwObject")
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
    /*using UnityEngine;
using System.Collections;

public class Grab_Throw : MonoBehaviour
{

    public bool grabbed;
    RaycastHit2D hit;
    public float distance = 2f;
    public Transform holdpoint;
    public float throwforce;
    public LayerMask notgrabbed;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
        
            if (!grabbed)
            {
                Physics2D.queriesStartInColliders = false;

                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                if (hit.collider != null && hit.collider.tag == "grabbable")
                {
                    grabbed = true;
                    Debug.Log("werkt");
                }
            }
            else if (!Physics2D.OverlapPoint(holdpoint.position, notgrabbed))
            {
                grabbed = false;

                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {

                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwforce;
                }
            }
        }
        if (grabbed)
            hit.collider.gameObject.transform.position = holdpoint.position;
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}*/
}