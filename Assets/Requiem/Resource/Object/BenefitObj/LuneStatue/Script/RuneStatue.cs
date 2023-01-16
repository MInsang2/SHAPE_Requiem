using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class RuneStatue : MonoBehaviour
{
    [SerializeField] Light2D[] m_DivArr;
    [SerializeField] Light2D[] m_circleLightArr;
    [SerializeField] Vector2 m_savePoint;
    [SerializeField] bool m_isActive;
    Animator m_animator;


    private void Awake()
    {
        if (m_savePoint == Vector2.zero)
        {
            m_savePoint = transform.position;
        }

        m_isActive = false;
        m_animator = GetComponent<Animator>();
        for (int i = 0; i < m_DivArr.Length; i++)
        {
            m_DivArr[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Lune && DataController.LuneActive)
        {
            DataController.PlayerSavePoint = m_savePoint;
            DataController.PlayerHP = DataController.PlayerMaxHP;

            if (!m_isActive)
            {
                m_animator.SetBool("IsActive", true);
                for (int i = 0; i < m_DivArr.Length; i++)
                {
                    m_DivArr[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < m_circleLightArr.Length; i++)
                {
                    m_circleLightArr[i].intensity = 0f;
                }
                m_isActive = true;
            }
        }
    }
}
