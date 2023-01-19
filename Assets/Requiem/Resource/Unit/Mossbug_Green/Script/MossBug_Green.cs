using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossBug_Green : MonoBehaviour
{
    // 이동 속도.
    // 빛 감지 범위
    // 초기 위치
    // 빛 감지 시 해당 빛으로 이동
    // 빛 없을 시 초기 위치로 이동

    [SerializeField] float m_speed;
    [SerializeField] float m_detectionRange;
    [SerializeField] Vector2 m_target;
    Collider2D m_collider;
    Vector2 m_origin;
    bool m_detectLight;
    RaycastHit2D[] m_light;
    int temp = 0;

    void Start()
    {
        m_origin = transform.position;
        m_target = m_origin;
        m_collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        DetectLihgt();
        TargetChange();
        MoveTarget();
    }

    void MoveTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_target, m_speed * Time.deltaTime);
        PlatformActive();
    }

    void DetectLihgt()
    {
        m_light = Physics2D.CircleCastAll(transform.position, m_detectionRange, Vector2.up, 0f);
        for (int i = 0; i < m_light.Length; i++)
        {
            if (m_light[i].collider.gameObject.layer == (int)LayerName.MossLight)
            {
                temp = i;
                break;
            }
            else
            {
                temp = -1;
            }
        }

        if (temp != -1)
        {
            m_detectLight = true;
            Debug.Log("m_detectLight = true");
        }
        else
        {
            m_detectLight = false;
            Debug.Log("m_detectLight = false");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_detectionRange);
    }

    void TargetChange()
    {
        if (temp == -1)
        {
            m_target = m_origin;
        }
        else
        {
            m_target = m_light[temp].transform.position;
        }
    }

    void PlatformActive()
    {
        if ((Vector2)transform.position == m_target)
        {
            m_collider.enabled = true;
        }
        else
        {
            m_collider.enabled = false;
        }
    }
}
