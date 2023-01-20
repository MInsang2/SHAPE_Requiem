using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossBug_Red : Enemy
{
    // 룬의 빛이랑 접촉하면, 플레이어에게 돌진
    // 일정거리 이상 벗어나면 초기위치로 복귀
    [SerializeField] float m_speed;
    [SerializeField] float m_returnDistance;
    Vector2 m_target;
    Vector2 m_origin;
    Transform m_player;

    bool m_detectLight = false;


    void Start()
    {
        m_name = "mossBug_Red";
        m_damage = 1;
        m_origin = transform.position;
        m_target = m_origin;
        m_player = GameObject.Find("player").transform;
    }

    void Update()
    {
        StatusUpdate();
        MoveTarget();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.LightArea)
        {
            m_detectLight = true;
        }
    }

    void StatusUpdate()
    {
        ChangeTarget();
        MissingPlayer();
    }

    void MoveTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_target, m_speed * Time.deltaTime);
    }

    void ChangeTarget()
    {
        if (m_detectLight)
        {
            m_target = m_player.position;
        }
        else
        {
            m_target = m_origin;

        }
    }

    void MissingPlayer()
    {
        if (((Vector2)transform.position - m_origin).magnitude > m_returnDistance)
        {
            m_detectLight = false;
        }
    }
}
