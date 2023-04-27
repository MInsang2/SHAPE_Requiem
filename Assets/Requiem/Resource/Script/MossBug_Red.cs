using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossBug_Red : Enemy_Dynamic
{
    // 룬의 빛이랑 접촉하면, 플레이어에게 돌진
    // 일정거리 이상 벗어나면 초기위치로 복귀
    [SerializeField] float speed;
    [SerializeField] float returnDistance;
    Vector2 target;
    Vector2 origin;
    Transform player;

    bool detectLight = false;


    void Start()
    {
        m_name = EnemyData.DynamicEnemyNameArr[2];
        damage = 1;
        origin = transform.position;
        target = origin;
        player = PlayerData.PlayerObj.transform;

        if (m_name == null) Debug.Log("m_name == null");
        if (origin == null) Debug.Log("origin == null");
        if (target == null) Debug.Log("target == null");
        if (player == null) Debug.Log("player == null");
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
            detectLight = true;
        }
    }

    void StatusUpdate()
    {
        ChangeTarget();
        MissingPlayer();
    }

    void MoveTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void ChangeTarget()
    {
        if (detectLight)
        {
            target = player.position;
        }
        else
        {
            target = origin;

        }
    }

    void MissingPlayer()
    {
        if (((Vector2)transform.position - origin).magnitude > returnDistance)
        {
            detectLight = false;
        }
    }
}
