using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    /// <summary>
    /// �߰� ���
    /// </summary>
    [SerializeField] Transform m_target;

    /// <summary>
    /// �÷��̾� ���� ������Ʈ �̸�
    /// </summary>
    [SerializeField] string m_playerName;
    
    /// <summary>
    /// �����̴� �ӵ�
    /// </summary>
    [SerializeField] float m_speed = 5f;
    [SerializeField] float m_escapeSpeed = 5f;

    /// <summary>
    /// �þ� ������
    /// </summary>
    [SerializeField] float m_sightArea = 3f;

    /// <summary>
    /// �߰� ������
    /// </summary>
    [SerializeField] float m_chaseArea = 10f;

    /// <summary>
    /// �þ� ����
    /// </summary>
    [SerializeField] Transform m_sight;

    /// <summary>
    /// �߰� ����
    /// </summary>
    [SerializeField] Transform m_chase;

    [SerializeField] Collider2D m_collider;

    /// <summary>
    /// �ʱ� ��ǥ
    /// </summary>
    Transform m_origin;

    /// <summary>
    /// m_origin.position�� �� ����ŭ ������ ����
    /// </summary>
    [SerializeField] float m_near;

    /// <summary>
    /// �� �ݶ�����
    /// </summary>
    public LayerMask LuneLayer; // the layer of the collider to escape from

    private Rigidbody2D m_rb;
    bool m_isChasing = false;
    bool m_isEscape = false;

    public Bat()
    {
        m_name = "Bat";
        m_damage = 1;
    }

    void Start()
    {
        m_target = FindObject().transform;
        // get the Rigidbody2D component on this game object
        m_rb = GetComponent<Rigidbody2D>();
        m_origin = new GameObject("BatOrigin").transform;
        m_origin.position = transform.position;
        m_chase.parent = null;
    }

    void Update()
    {
        BatStateUpdate();

        if (!m_isEscape)
        {
            if (m_isChasing)
            {
                ChasePlayer();
            }
            else
            {
                ReturnOrigin();
            }
        }
    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        // check if the collider is on the specified layer
        if (collision.gameObject.layer == (int)LayerName.LightArea)
        {
            m_collider.isTrigger = false;
            m_isEscape = true;
            // calculate a direction to move the object away from the collider
            Vector2 escapeDirection = (transform.position - collision.transform.position).normalized;

            // move the object out of the scope of the collider
            m_rb.velocity = escapeDirection * m_escapeSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.LightArea)
        {
            m_isEscape = false;
        }
    }

    void BatStateUpdate()
    {
        SightSearch();
        ChaseSearch();
        DrawSightArea();
        DrawChaseArea();
        ChangeRotate();
    }

    void ChasePlayer()
    {
        if (m_target == null)
        {
            Debug.Log("m_target == null");
            return;
        }

        if (m_isChasing)
        {
            m_collider.isTrigger = false;
            Vector2 direction = (m_target.position - transform.position).normalized;

            m_rb.velocity = direction * m_speed;
        }
    }
    
    void ReturnOrigin()
    {
        if (m_origin == null)
        {
            Debug.Log("m_origin == null");
            return;
        }

        if (Vector2.Distance(transform.position, m_origin.position) < m_near)
        {
            m_rb.velocity = Vector2.zero;
        }
        else
        {
            m_collider.isTrigger = true;
            Vector2 direction = (m_origin.position - transform.position).normalized;

            m_rb.velocity = direction * m_speed;
        }
    }

    void SightSearch()
    {
        if (Vector2.Distance(transform.position, m_target.position) < m_sightArea)
            m_isChasing = true;
    }

    void ChaseSearch()
    {
        if (Vector2.Distance(m_origin.position, m_target.position) > m_chaseArea)
            m_isChasing = false;
    }


    void DrawSightArea()
    {
        m_sight.localScale = new Vector2(m_sightArea, m_sightArea);
    }

    void DrawChaseArea()
    {
        m_chase.localScale = new Vector2(m_chaseArea, m_chaseArea);
    }

    public GameObject FindObject()
    {
        GameObject obj = GameObject.Find(m_playerName);

        return obj ? obj : null;
    }

    void ChangeRotate()
    {
        if (m_rb.velocity.x > 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }
}
