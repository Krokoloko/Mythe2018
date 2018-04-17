using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour {
    //booleans which layer they should adjust to

    /*------------------Layers------------------
     0 = default: works on every layer.
     8 = layer1: holds no collision with 'layer2' & 'goingthrough'
     20 = goingThrough: this can go through 'layer1'
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
            switch (_id)
            {
                case "Player":
                    Debug.Log("object is player");
                    if (gameObject.GetComponent<Climb>().state != Climb.ClimbState.none)
                    {
                        switch (gameObject.GetComponent<Climb>().state)
                        {
                            case Climb.ClimbState.climbingDown:
                                gameObject.layer = 20;
                                break;

                            case Climb.ClimbState.climbingNeutral:
                                gameObject.layer = 20;
                                break;

                            case Climb.ClimbState.climbingUp:
                                gameObject.layer = 20;
                                break;
                        }
                    }
                    else
                    {
                        gameObject.layer = 0;
                    }
                    switch (gameObject.GetComponent<Jump>().state)
                    {
                        case Jump.jumpState.rising:
                            Debug.Log("layer change from rising");
                            gameObject.layer = 20;
                            break;
                        case Jump.jumpState.midair:
                            gameObject.layer = 20;
                            break;
                        case Jump.jumpState.landing:
                            Debug.Log("layer change from landing");
                            gameObject.layer = 0;
                            break;
                        case Jump.jumpState.falling:
                            Debug.Log("layer change from falling");
                            gameObject.layer = 20;
                            break;
                        case Jump.jumpState.none:
                            Debug.Log("layer change from nothing");
                            gameObject.layer = 0;
                            break;
                    }

                    
                    break;
                 case "enemy":
                    switch (gameObject.GetComponent<EnemyWalker>().AirState)
                    {
                        case Enemy.EnemyAirState.falling:
                            gameObject.layer = 20;
                            break;

                        case Enemy.EnemyAirState.midair:
                            gameObject.layer = 20;
                            break;

                        case Enemy.EnemyAirState.none:
                            gameObject.layer = 0;
                            break;

                        case Enemy.EnemyAirState.landing:
                            gameObject.layer = 0;
                            break;
                    }
                    break;
            }
        }
    }
}
