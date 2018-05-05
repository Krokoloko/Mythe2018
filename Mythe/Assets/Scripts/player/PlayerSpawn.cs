using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour {
    private bool dead = false;
    private GameObject _player;
    [Tooltip("Hoeft geen waarde mee te geven.")]
    public GameObject levelManager;
    [Tooltip("Transform moet transform van zich zelf zijn.")]
    public Transform SpawnPoint;
    
	void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("levelManager");
        _player = GameObject.FindGameObjectWithTag("Player");
        _player.transform.position = SpawnPoint.transform.position;
    }

	public void OnDead()	
	{
		levelManager.GetComponent<LevelManager> ().Reload ();
		_player.transform.position = SpawnPoint.position;
	}
}
