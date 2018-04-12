using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject[] levels;
    private int _level;

    void Start()
    {
        _level = 0;
    }

    public void GoToNextLevel() 
    {
        _level++;
        Instantiate<GameObject>(levels[0]);
    }

    private bool LevelLoaded(int index)
    {
        return (levels[index].GetComponent<Renderer>().isVisible);
    }

	void Update ()
    {
        if (LevelLoaded(_level))
        {

        }
	}
}
