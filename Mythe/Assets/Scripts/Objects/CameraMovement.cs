using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Transform leftBound;
    public Transform rightBound;
    public Transform player;
    [SerializeField]
    private Vector3 offset;
     void Start()
    {
        transform.position = new Vector3 (player.position.x,player.position.y + 2, player.position.z - 10);
        offset = transform.position - target.position;
    }

    void Update()
    {
        transform.position = target.position + offset;
        Debug.Log("clamp = " + Mathf.Clamp(player.position.x, leftBound.position.x, rightBound.position.x) + " leftBound = " + leftBound.position.x + " rightBound = " + rightBound.position.x);
        transform.position = new Vector3( Mathf.Clamp(player.position.x, leftBound.position.x, rightBound.position.x), transform.position.y, transform.position.z);
    }
}
