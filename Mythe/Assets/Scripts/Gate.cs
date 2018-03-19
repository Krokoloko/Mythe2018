using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    public GameObject button;
    private Button _button;
    private Transform _origin;
    private float _limit;
    public string direction;
    public float distance = 3;

    private void OnButton()
    {
        if (_button.active)
        {
            switch (direction)
            {
                case"up":
                    if (_limit >= transform.position.y)
                    {
                        transform.Translate(Vector3.up*2*Time.deltaTime);
                    }
                    else
                    {
                        print("stop");
                    }
                    break;
                case"down":
                    transform.Translate(Vector3.down);
                    break;
                case"left":
                    transform.Translate(Vector3.left);
                    break;
                case "right":
                    transform.Translate(Vector3.right);
                    break;
            }
        }
    }

	// Use this for initialization
	void Start () {
        _origin = transform;
        _limit = _origin.position.y + distance;
        _button = button.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
        OnButton();
	}
}
