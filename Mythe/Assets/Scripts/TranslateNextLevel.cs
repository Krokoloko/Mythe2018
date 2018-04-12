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

    public void ReplaceCameraValues()
    {
        for (int i =0;i < _level.GetComponentsInChildren<GameObject>().Length; i++)
        {
            if (_level.GetComponentsInChildren<GameObject>()[i].tag == "leftBound")
            {
                _camera.leftBound = _level.GetComponentsInChildren<GameObject>()[i].transform;
            }
            if (_level.GetComponentsInChildren<GameObject>()[i].tag == "rightBound")
            {
                _camera.rightBound = _level.GetComponentsInChildren<GameObject>()[i].transform;
            }
        }
    }
}
