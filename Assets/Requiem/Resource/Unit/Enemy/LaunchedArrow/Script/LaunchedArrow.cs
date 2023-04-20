using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchedArrow : Enemy
{
    [SerializeField] float m_destroyTime;
    [SerializeField] float m_speed;
    Rigidbody2D m_rigid;
    Vector2 m_dir;
    Vector2 m_origin;

    void Start()
    {
        m_name = EnemyData.DynamicEnemyNameArr[1];
        m_damage = 1;
        m_rigid = GetComponent<Rigidbody2D>();
        m_origin = transform.position;
    }

    private void Update()
    {
        MoveArrow();
        Invoke("ArrowDestroy", m_destroyTime);
    }

    

    public override void TriggerOn()
    {

    }

    public void ArrowDestroy()
    {
        Destroy(gameObject);
    }

    void MoveArrow()
    {
        m_rigid.velocity = m_dir * m_speed;
    }

    public void SetArrowDir(Vector2 _dir)
    {
        m_dir = _dir;
    }

    public void SetRotation(Quaternion _quaternion)
    {
        transform.rotation = _quaternion;
    }
}
