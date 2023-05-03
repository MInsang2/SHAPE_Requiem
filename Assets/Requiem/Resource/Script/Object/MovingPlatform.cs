using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Switch m_swich;
    [SerializeField] Collider2D m_Collider;
    [SerializeField] SpriteRenderer m_platform;
    [SerializeField] Sprite m_platformActive;
    [SerializeField] Sprite m_platformInactive;
    [SerializeField] GameObject m_Light1;
    [SerializeField] GameObject m_Light2;
    [SerializeField] AudioSource m_audioSource;
    [SerializeField] AudioClip m_clip;

    bool m_isActivated = false;

    public bool IsActivated
    {
        get { return m_isActivated; }
        set
        {
            if (value != m_isActivated)
            {
                m_isActivated = value;
                OnActivationChanged();
            }
        }
    }

    private void Awake()
    {
        m_Light2.SetActive(false);
    }

    void Update()
    {
        FlatformMove();
    }

    void OnActivationChanged()
    {
        m_audioSource.PlayOneShot(m_clip);
    }

    void FlatformMove()
    {
        if (m_swich.isActive)
        {
            m_platform.sprite = m_platformInactive;
            m_Collider.enabled = false;

            if (m_Light1 != null)
                m_Light1.SetActive(false);

            if (m_Light2 != null)
                m_Light2.SetActive(true);
        }
        else if (!m_swich.isActive)
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
