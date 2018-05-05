using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateNextLevel : MonoBehaviour {

    private GameObject _level;
    private CameraMovement _camera;

    public TranslateNextLevel(GameObject level, CameraMovement camera)
    {
        _camera = camera;
        _level = level;
    }
    public TranslateNextLevel(Transform leftBound, Transform rightBound, Transform playerSpawn,CameraMovement camera)
    {
        _camera = camera;
        _camera.leftBound = leftBound;
        _camera.rightBound = rightBound;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpawn>().SpawnPoint = playerSpawn;
    }

    public void ReplaceCameraValues()
    {
        int objects = _level.transform.childCount;
        for (int i =0;i < objects; i++)
        {
            if (_level.transform.GetChild(i).tag == "LeftBound")
            {
                _camera.leftBound = _level.transform.GetChild(i).transform;
            }
            if (_level.transform.GetChild(i).tag == "RightBound")
            {
                _camera.rightBound = _level.transform.GetChild(i).transform;
            }
        }
    }
}
