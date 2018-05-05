using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : InteractableObject
{
    [SerializeField]private float throwforce = 5;
    private float offset;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected override void InteractionStart()
    {
        offset = player.position.x - transform.position.x;
    }

    protected override void InteractionEnd()
    {
        Debug.Log("lost ball touch");
        if (CheckSideRaycast(player.transform.position))
        {

            Debug.Log("Player is throwing to the right side");
            this.GetComponent<Rigidbody>().AddForce(new Vector3(-0.4f, 1.1f, 0) * throwforce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("Player is throwing to the left side");
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0.4f, 1.1f, 0) * throwforce, ForceMode.Impulse);
        }
    }

    protected override bool ReasonInteraction()
    {
        /*  if (Input.GetKey(KeyCode.LeftShift))
              return true; */

        return Input.GetKey(KeyCode.LeftShift);
    }

    protected override void InteractionAction()
    {
        Vector3 targetPosition = new Vector3(player.position.x - offset, transform.position.y, transform.position.z);
        transform.position = targetPosition;
    }
    private bool CheckSideRaycast(Vector3 origin)
    {
        if (origin.x > this.transform.position.x)
            return true;
        else return false;
    }
}
