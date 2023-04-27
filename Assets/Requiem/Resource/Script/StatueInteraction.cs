using System.Collections;
using UnityEngine;
using DG.Tweening;

public class StatueInteraction: MonoBehaviour
{
    [SerializeField] Transform m_luneStatue;
    // the target point to rotate around
    public Vector2 targetPoint;
    public float m_moveTime;
    // the speed at which to rotate, in degrees per second
    public float rotationSpeed = 180f;

    private void Start()
    {
        targetPoint = m_luneStatue.position;
    }

    void Update()
    {
        transform.RotateAround(targetPoint, Vector3.back, rotationSpeed);
        transform.DOMove(targetPoint, m_moveTime);
    }
}
