using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 1f;

    private void Start()
    {
        pointA = transform.Find("PointA");
        pointB = transform.Find("PointB");

        pointA.parent = null;
        pointB.parent = null;
    }

    private void Update()
    {
        float pingPongTime = Mathf.PingPong(Time.time * moveSpeed, 1);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, pingPongTime);
    }
}
