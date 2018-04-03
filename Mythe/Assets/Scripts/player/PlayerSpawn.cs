using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour {
    private bool dead = false;
    public GameObject player;
    public Transform SpawnPoint;
     void Start()
    {
        player.transform.position = SpawnPoint.transform.position;
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            dead = true;
        }
        if (dead)
        {
            //Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene(1);
        }
    }
}
