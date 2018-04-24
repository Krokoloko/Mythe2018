using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public bool active = false;
    //public float distance 1;
  //  public Direction direction;


void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "MoveAble" || col.gameObject.tag == "ThrowObject" || col.gameObject.tag == "Enemy")
        {
            active = true;
            print("nyess");

        }
    }
void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "MoveAble" || col.gameObject.tag == "ThrowObject" || col.gameObject.tag == "Enemy")
        {
            active = false;
            print("nuuo");

        }
    }




    public GameObject button;
    public enum Direction { up, down, left, right };
    public Direction direction;
    public float distance = 0.5f;

    private Vector3 _startPosition;
    private Button _button;
    private Transform _origin;
    private float _limit;

    // Use this for initialization
    void Start()
    {
        //_origin = transform;
        //_limit = transform.position.y;
        //origin.position.y - distance;
        _button = button.GetComponent<Button>();
        _startPosition = transform.position;
    }

    private void OnButton()
    {
        if (_button.active)
        {
            switch (direction)
            {
                case Direction.up:
                    if (_startPosition.y + distance >= transform.position.y)
                    {
                        transform.Translate(Vector3.up * 2 * Time.deltaTime);
                    }
                    break;
                case Direction.down:
                    if (_startPosition.y - distance <= transform.position.y)
                    {
                        transform.Translate(Vector3.down * 2 * Time.deltaTime);
                    }
                    break;
                case Direction.right:
                    if (_startPosition.x + distance >= transform.position.x)
                    {
                        transform.Translate(Vector3.right * 2 * Time.deltaTime);
                    }
                    break;
                case Direction.left:
                    if (_startPosition.x - distance <= transform.position.x)
                    {
                        transform.Translate(Vector3.left * 2 * Time.deltaTime);
                    }
                    break;
            }
        }

        else //if(!_button.active)
        {

            switch (direction)
            {
                case Direction.up:
                    if (transform.position.y >= _startPosition.y)
                    {
                        transform.Translate(Vector3.down * 2 * Time.deltaTime);
                    }
                    break;
                case Direction.down:
                    if (transform.position.y <= _startPosition.y)
                    {
                        transform.Translate(Vector3.up * 2 * Time.deltaTime);
                    }
                    break;
                case Direction.right:
                    if (transform.position.x >= _startPosition.x)
                    {
                        transform.Translate(Vector3.left * 2 * Time.deltaTime);
                    }
                    break;
                case Direction.left:
                    if (transform.position.x <= _startPosition.x)
                    {
                        transform.Translate(Vector3.right * 2 * Time.deltaTime);
                    }
                    break;
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        OnButton();
    }
}


//}
