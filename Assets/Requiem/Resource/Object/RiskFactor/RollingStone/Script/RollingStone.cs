using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStone : RiskFactor
{
    Rigidbody2D m_rigid;
    Vector2 m_origin;

    void Start()
    {
        m_name = "rollingStone";
        m_damage = 1;
        m_rigid = GetComponent<Rigidbody2D>();
        m_rigid.bodyType = RigidbodyType2D.Kinematic;
        m_origin = transform.position;
    }

    void Update()
    {
        
    }

    public override void TriggerOn()
    {
        transform.parent = null;
        m_rigid.bodyType = RigidbodyType2D.Dynamic;
        m_rigid.freezeRotation = false;
        Debug.Log("active");
    }

    public void resetPosition()
    {
        m_rigid.bodyType = RigidbodyType2D.Kinematic;
        m_rigid.velocity = Vector2.zero;
        m_rigid.freezeRotation = true;
        transform.position = m_origin;

    }
}
