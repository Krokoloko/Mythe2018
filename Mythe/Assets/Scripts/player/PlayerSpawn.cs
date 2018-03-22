using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour {
    private bool dead = false;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            dead = true;
        }
        if (dead)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
