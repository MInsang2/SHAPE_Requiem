using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Swich m_swich;
    [SerializeField] Collider2D m_Collider;
    [SerializeField] SpriteRenderer m_platform;
    [SerializeField] Sprite m_platformActive;
    [SerializeField] Sprite m_platformInactive;
    [SerializeField] GameObject m_Light1;
    [SerializeField] GameObject m_Light2;

    private void Awake()
    {
        m_Light2.SetActive(false);
    }

    void Update()
    {
        FlatformMove();
    }

    void FlatformMove()
    {
        if (m_swich.m_isActive)
        {
            m_platform.sprite = m_platformInactive;
            m_Collider.enabled = false;

            if (m_Light1 != null)
                m_Light1.SetActive(false);

            if (m_Light2 != null)
                m_Light2.SetActive(true);
        }
        else if (!m_swich.m_isActive)
        {
            m_platform.sprite = m_platformActive;
            m_Collider.enabled = true;

            if (m_Light1 != null)
                m_Light1.SetActive(true);

            if (m_Light2 != null)
                m_Light2.SetActive(false);
        }
    }
}
