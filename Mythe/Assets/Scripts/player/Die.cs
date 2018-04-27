using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {

	private float _orgHeight;
	private bool _enemy = false; 
	public float deathHeight;

	void Start()
	{
		_orgHeight = transform.position.y;
	}

	void Update()
	{
		if (OnDeathLocation() || _enemy)
			GameObject.FindGameObjectWithTag ("playerSpawnPoint").GetComponent<PlayerSpawn> ().OnDead ();
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "enemy")
			_enemy = true;
	}

	void OnCollisionExit(Collision col)	
	{
		_enemy = false;
	}

	private bool OnDeathLocation()
	{
		Debug.Log(transform.position.y - _orgHeight);

		if (transform.position.y < _orgHeight - Mathf.Abs(deathHeight))
			return true;
		else
			return false;
	}
}
