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
        transform.position = new Vector3 (player.transform.position.x,player.transform.position.y, player.transform.position.z - 10);
        offset = transform.position - target.transform.position;
    }

    void Update()
    {
        transform.position = target.transform.position + offset;
        transform.position = new Vector3( Mathf.Clamp(transform.position.x, leftBound.transform.position.x, rightBound.transform.position.x), transform.position.y, transform.position.z);
    }
}
