using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    private GameObject _player;
    public bool active;

	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (!GetComponent<BoxCollider>().isTrigger)
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player is in range");
            active = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            active = false;
        }
    }
}
