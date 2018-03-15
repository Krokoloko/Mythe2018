using UnityEngine;
using System.Collections;

public class Grab_Throw : MonoBehaviour
{

    public bool grabbed;
    RaycastHit hit;
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
                /*Physics.queriesStartInColliders = false;*/

                hit = Physics.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                if (hit.collider != null && hit.collider.tag == "grabbable")
                {
                    grabbed = true;
                    Debug.Log("werkt");
                }
            }
            else if (!Physics2D.OverlapPoint(holdpoint.position, notgrabbed))
            {
                grabbed = false;

                if (hit.collider.gameObject.GetComponent<Rigidbody>() != null)
                {

                    hit.collider.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(transform.localScale.x, 1,0) * throwforce;
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