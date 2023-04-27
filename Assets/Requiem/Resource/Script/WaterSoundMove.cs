using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSoundMove : MonoBehaviour
{
    public Transform targetObject;
    public Transform PointA;
    public Transform PointB;

    public float minY;
    public float maxY;

    private void Start()
    {
        targetObject = PlayerData.PlayerObj.transform;
        PointA = transform.Find("PointA");
        PointB = transform.Find("PointB");

        minY = PointB.position.y;
        maxY = PointA.position.y;

        PointA.parent = null;
        PointB.parent = null;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        float targetY = targetObject.position.y;

        currentPosition.y = Mathf.Clamp(targetY, minY, maxY);
        transform.position = currentPosition;
    }
}
