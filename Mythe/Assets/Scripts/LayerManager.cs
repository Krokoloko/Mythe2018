﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour {
    //booleans which layer they should adjust to

    /*------------------Layers------------------
     0 = default: works on every layer.
     8 = layer1: holds all layers except 'layer1'
     9 = layer2: holds all layers except 'layer2' (objects such as jumpthroughplatforms are associated with this layer)
         */
    public bool jumpThroughPlatfroms = false;


    private string _id;
    private int _orgLayer;
    private Ray _ray;

	void Start ()
    {
        _orgLayer = gameObject.layer;
        _id = gameObject.tag;
	}
	
	void Update ()
    {
        _ray = new Ray(transform.position, Vector3.down);
        jumpThrough(jumpThroughPlatfroms);
    }

    private void jumpThrough(bool b)
    {
        if (b)
        {
            if (_id == "Player")
            {
                switch (gameObject.GetComponent<Jump>().state)
                {
                    case Jump.jumpState.rising:
                        gameObject.layer = 8;
                        break;
                    case Jump.jumpState.falling:
                        gameObject.layer = 0;
                        break;
                    case Jump.jumpState.none:
                        gameObject.layer = 0;
                        break;
                }
            }
        }
    }
}