using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class UpDownPlatform : MonoBehaviour
{
    public static Action a_Initialized;

    [SerializeField] float m_transPos; // 어디까지 움직이는가
    [SerializeField] float m_moveSpeed; // 움직이는 속도
    [SerializeField] bool m_moveDir; // 움직이는 방향 . 기본값 +
    [SerializeField] bool m_DirY; // x축, y축 결정 . 기본값 x

    [Header("falling Platform")]
    public Vector2 m_resetPoint;

    Swich m_swich;
    Transform m_wall;

    float m_pos;
    float m_posOrigin; // 벽 초기 좌표
    float m_movePos; // 움직인 거리

    private void Awake()
    {
        a_Initialized = () => { Initialized(); };

        m_swich = transform.GetChild(0).GetComponent<Swich>();
        m_wall = transform.GetChild(1);
        m_movePos = 0f;

        if (m_DirY)
        {
            m_pos = m_wall.position.y;
            m_posOrigin = m_wall.position.y;
        }
        else
        {
            m_pos = m_wall.position.x;
            m_posOrigin = m_wall.position.x;
        }
        
    }

    void Update()
    {
        DirCheck();
    }

    void DirCheck()
    {
        if (m_DirY == false)
        {
            switch (m_moveDir)
            {
                case true:
                    MovePlatformPlusX();
                    break;
                case false:
                    MovePlatformMinusX();
                    break;
            }
        }
        else if (m_DirY == true)
        {
            switch (m_moveDir)
            {
                case true:
                    MovePlatformPlusY();
                    break;
                case false:
                    MovePlatformMinusY();
                    break;
            }
        }
    }

    void MovePlatformPlusX()
    {
        if (m_swich.m_isActive && m_movePos <= m_transPos)
        {
            m_wall.position = new Vector3(m_pos, m_wall.position.y, 0f);
            m_pos += m_moveSpeed * Time.deltaTime;
            m_movePos += m_moveSpeed * Time.deltaTime;
        }
        else if (!m_swich.m_isActive && m_movePos > m_transPos)
        {
            m_wall.position = new Vector3(m_pos, m_wall.position.y, 0f);
            m_pos -= m_moveSpeed * Time.deltaTime;
            m_movePos -= m_moveSpeed * Time.deltaTime;
        }
    }

    void MovePlatformMinusX()
    {
        if (m_swich.m_isActive && m_movePos <= m_transPos)
        {
            m_wall.position = new Vector3(m_pos, m_wall.position.y, 0f);
            m_pos -= m_moveSpeed * Time.deltaTime;
            m_movePos += m_moveSpeed * Time.deltaTime;
        }
        else if (!m_swich.m_isActive && m_movePos > 0f)
        {
            m_wall.position = new Vector3(m_pos, m_wall.position.y, 0f);
            m_pos += m_moveSpeed * Time.deltaTime;
            m_movePos -= m_moveSpeed * Time.deltaTime;
        }
    }

    void MovePlatformPlusY()
    {
        if (m_swich.m_isActive && m_movePos <= m_transPos)
        {
            m_wall.position = new Vector3(m_wall.position.x, m_pos, 0f);
            m_pos += m_moveSpeed * Time.deltaTime;
            m_movePos += m_moveSpeed * Time.deltaTime;
        }
        else if (!m_swich.m_isActive && m_movePos > 0f)
        {
            m_wall.position = new Vector3(m_wall.position.x, m_pos, 0f);
            m_pos -= m_moveSpeed * Time.deltaTime;
            m_movePos -= m_moveSpeed * Time.deltaTime;
        }
    }

    void MovePlatformMinusY()
    {
        if (m_swich.m_isActive && m_movePos <= m_transPos)
        {
            m_wall.position = new Vector3(m_wall.position.x, m_pos, 0f);
            m_pos -= m_moveSpeed * Time.deltaTime;
            m_movePos += m_moveSpeed * Time.deltaTime;
        }
        else if (!m_swich.m_isActive && m_movePos > 0f)
        {
            m_wall.position = new Vector3(m_wall.position.x, m_pos, 0f);
            m_pos += m_moveSpeed * Time.deltaTime;
            m_movePos -= m_moveSpeed * Time.deltaTime;
        }
    }

    void Initialized()
    {
        if (m_DirY)
        {
            m_wall.position = new Vector2(transform.position.x, m_posOrigin);
        }
        else
        {
            m_wall.position = new Vector2(m_posOrigin, transform.position.y);
        }

        m_movePos = 0f;
    }
}
