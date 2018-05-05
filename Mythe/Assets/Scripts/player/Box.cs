using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : InteractableObject
{
    private Rigidbody _rb;
    private float offset;

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
        throw new System.NotImplementedException();
    }

    protected override bool ReasonInteraction()
    {
        if (IsPlayerOnBox()) return false;

        return (Input.GetKey(KeyCode.LeftShift));
    }

    protected override void InteractionAction()
    {
        Vector3 targetPosition = new Vector3(player.position.x - offset, transform.position.y, transform.position.z);
        transform.position = targetPosition;    
    }

    private bool IsPlayerOnBox()
    {
        Vector3 rayOriginLeft = new Vector3(player.position.x - (player.localScale.x / 2f), player.position.y, player.position.z);
        Vector3 rayOriginRight = new Vector3(player.position.x + (player.localScale.x / 2f), player.position.y, player.position.z);

        bool leftHit = CheckSideRaycast(rayOriginLeft);
        bool rightHit = CheckSideRaycast(rayOriginRight);

        print("left: " + leftHit);
        print("right: " + rightHit);

        if (leftHit == true || rightHit == true)
            return true;

        return false;
    }

    private bool CheckSideRaycast(Vector3 origin)
    {
        Ray ray = new Ray(origin, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1f))
        {
            if (hit.transform == this.transform)
                return true;
        }
        return false;
    }
}