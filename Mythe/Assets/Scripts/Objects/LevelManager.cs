using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//This manages the order, the spawning and the despawning of the levels

public class LevelManager : MonoBehaviour {

    //Note: these level prefabs will need to be measured with a left and a right transform location so that the level spawns correctly.
    [Header("Place your level prefabs here")]
    public GameObject[] levels;
    [Header("Within the range the level is going to load.")]
    public float radius;

    private GameObject _currentLevel;
    public GameObject _levelToDestroy;
    private TranslateNextLevel _cameraTransition;
    public Transform _leftBoundry, _rightBoundry, _leftEnd, _rightEnd, _playerSpawn;
    private Sprite _playerSprite;
    private bool _switch = false;
    private int _level;

    void Start()
    {
        _level = 0;

        //Note: The first level will always spawn at the (0,0,0) position.
        _currentLevel = Instantiate<GameObject>(levels[_level], new Vector3(0,0,0), new Quaternion()) as GameObject;
        SetBoundries();
        _playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sprite;
        _cameraTransition = new TranslateNextLevel(_leftBoundry,_rightBoundry, _playerSpawn,GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>());

    }

    void Update()
    {
        
        AdjustPos();
        
        //Is it at the end of the level so that the next level can spawn
        //Debug.Log("_rightEnd: " + _rightEnd.transform.position + " - " + "player: " + GameObject.FindGameObjectWithTag("Player").transform.position);
        if ((_rightEnd.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).sqrMagnitude < radius * radius && !_switch)
        {
            Debug.Log(gameObject+" in range");
            _switch = true;
            GoToNextLevel();
            _cameraTransition.ReplaceCameraValues();
        }

        Debug.Log("switch" + _switch);
        //Is the player far enough from the previous level so it can be despawned.
        if (
            GameObject.FindGameObjectWithTag("Player").transform.position.x > _rightEnd.position.x + radius + _playerSprite.rect.width &&
            _switch)
        {
            Debug.Log(gameObject + " out range");

            Destroy(_levelToDestroy);
            if (GameObject.FindGameObjectsWithTag("LeftBound").Length == 1)
            {
                Debug.Log("previous level is destroyed");
                SetBoundries();
                _switch = false;

            }
        }
    }

    //Spawns the next level
    public void GoToNextLevel()
    {
        Transform prevRightEnd = GameObject.FindGameObjectWithTag("rightEnd").transform;

        float xPlacement, yPlacement = 0, zPlacement = 0;
        _levelToDestroy = _currentLevel;

        _level++;

        //If there are no levels to continue on, it will load a scene
        if (_level >= levels.Length) SceneManager.LoadScene(0);
        else
        {
            _cameraTransition = new TranslateNextLevel(levels[_level], GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>());
            //Debug.Log("rightbound x = " + _rightBoundry.transform.position.x + " | _leftEnd x = " + _leftEnd.transform.position.x + " | _rightEnd x = " + _rightEnd.transform.position.x);

                                //offset  \/                          start position  \/
            xPlacement = ((_rightEnd.position.x- _leftEnd.position.x)/2 + prevRightEnd.position.x/2);

            _currentLevel = Instantiate<GameObject>(levels[_level], new Vector3(xPlacement, yPlacement, zPlacement), new Quaternion())as GameObject;
            _playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sprite;

        }
    }
    //Adjusts to the player position
    private void AdjustPos()
    {
        IfTransformsNull();
        _leftEnd.transform.TransformPoint(_leftEnd.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z);
        _rightEnd.transform.TransformPoint(_leftEnd.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z);
    }

	public void Reload()
	{
		_levelToDestroy = _currentLevel;
        _currentLevel = Instantiate<GameObject>(levels[_level], _levelToDestroy.transform.position, new Quaternion()) as GameObject;
        
        Destroy(_levelToDestroy);
        SetBoundries();

	}

    //Reinits the boundries from the next level.
    private void SetBoundries()
    {
        _leftBoundry = GameObject.FindGameObjectWithTag("LeftBound").transform;
        _rightBoundry = GameObject.FindGameObjectWithTag("RightBound").transform;
        _leftEnd = GameObject.FindGameObjectWithTag("leftEnd").transform;
        _rightEnd = GameObject.FindGameObjectWithTag("rightEnd").transform;
        _playerSpawn = GameObject.FindGameObjectWithTag("playerSpawnPoint").transform;
    }

    private void IfTransformsNull()
    {
        if (_leftEnd == null && _rightEnd == null && _leftBoundry == null && _rightBoundry == null && _playerSpawn == null)
        {
            SetBoundries();
            _cameraTransition = new TranslateNextLevel(_leftBoundry, _rightBoundry, _playerSpawn, GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>());

            _cameraTransition.ReplaceCameraValues();
        }
    }
}
