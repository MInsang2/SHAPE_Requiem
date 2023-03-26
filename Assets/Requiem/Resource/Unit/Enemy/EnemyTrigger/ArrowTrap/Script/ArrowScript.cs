using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : Enemy
{
    [SerializeField] float m_destroyTime;
    [SerializeField] float m_speed;
    Rigidbody2D m_rigid;

    private void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>(); 
        m_name = "ArrowScript";
        m_rigid.velocity = transform.right * m_speed;
        Invoke("ArrowDestroy", m_destroyTime);
    }

    public override void TriggerOn()
    {

    }

    public void ArrowDestroy()
    {
        Destroy(gameObject);
    }

    public void SetRotation(Quaternion _quaternion)
    {
        transform.rotation = _quaternion;
    }

    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_damage = 1;
            Destroy(gameObject);
        }
    }
}
