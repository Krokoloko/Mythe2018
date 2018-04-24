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
}
