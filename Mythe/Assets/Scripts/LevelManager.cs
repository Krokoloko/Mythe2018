using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [Header("Place your level prefabs here")]
    public GameObject[] levels;
    [Header("Within the range the level is going to load.")]
    public float radius;
    private GameObject _currentLevel;
    private TranslateNextLevel _cameraTransition;
    private Transform _leftBoundry, _rightBoundry, _leftEnd, _rightEnd;
    private bool _switch = false;
    private int _level;

    void Start()
    {
        _level = 0;
    }

    void Update()
    {
        if ((GameObject.FindGameObjectWithTag("rightEnd").transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).sqrMagnitude < radius * radius && !_switch)
        {
            Debug.Log("in range");
            _switch = true;
            GoToNextLevel();
        }
        if ((GameObject.FindGameObjectWithTag("rightEnd").transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).sqrMagnitude > radius * radius && _switch)
        {
            Debug.Log("out range");
            _switch = false;
        }
    }

    public void GoToNextLevel() 
    {
        Transform previousLevel = GameObject.FindGameObjectWithTag("RightBound").transform;

        float xPlacement, yPlacement = levels[_level].transform.position.y, zPlacement = levels[_level].transform.position.z;

            
        for (int i = 0; i < levels[_level].transform.childCount; i++)
        {
            if (levels[_level].transform.GetChild(i).CompareTag("LeftBound"))
            {
                _leftBoundry = levels[_level].transform.GetChild(i).transform;
            }
            if (levels[_level].transform.GetChild(i).CompareTag("leftEnd"))
            {
                _leftEnd = levels[_level].transform.GetChild(i).transform;
            }
            if (levels[_level].transform.GetChild(i).CompareTag("RightBound"))
            {
                _rightBoundry = levels[_level].transform.GetChild(i).transform;
            }
            if (levels[_level].transform.GetChild(i).CompareTag("rightEnd"))
            {
                _rightEnd = levels[_level].transform.GetChild(i).transform;
            }
        }

        _level++;
        Debug.Log(_level);
        if (_level > levels.Length) SceneManager.LoadScene(0);
        else
        {
            _cameraTransition = new TranslateNextLevel(levels[_level], GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>());
            Debug.Log("rightbound = " + _rightBoundry + " | _leftEnd = " + _leftEnd + " | _rightEnd = " + _rightEnd);
            xPlacement = previousLevel.position.x + (_leftEnd.position.x - _rightEnd.position.x);

            _currentLevel = Instantiate<GameObject>(levels[_level], new Vector3(xPlacement, yPlacement, zPlacement), new Quaternion());
            Sprite playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sprite;
            while (GameObject.FindGameObjectWithTag("Player").transform.position.x >= _rightEnd.position.x + playerSprite.rect.width) Debug.Log("player is not on the other level");

            _cameraTransition.ReplaceCameraValues();
        }
    }

    private bool LevelLoaded(int index)
    {
        return (levels[index].GetComponent<Renderer>().isVisible);
    }
}
