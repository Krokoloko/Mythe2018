﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public LayerMask layer;
    public float forceAmount;
    private Vector3 overlapBoxPosition;

	void Start () {
        overlapBoxPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
	}
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            ApplyBounce();
    }
	void ApplyBounce()
    {
        var colsInBox = Physics.OverlapBox(overlapBoxPosition, Vector3.one / 2f, Quaternion.identity, layer);
        foreach(Collider c in colsInBox)
        {
            Rigidbody r = c.GetComponent<Rigidbody>();
            r.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(overlapBoxPosition, Vector3.one);
    }
}
