using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;


public class RuneStatue : MonoBehaviour
{
    public static Action a_Initialized;

    [SerializeField] Light2D[] m_DivArr;
    [SerializeField] Light2D[] m_circleLightArr;
    [SerializeField] Vector2 m_savePoint;
    [SerializeField] bool m_isActive;
    [SerializeField] AudioClip m_audioClip;
    Animator m_animator;
    AudioSource m_audioSource;


    private void Awake()
    {
        a_Initialized = () => { Initialized(); };

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

        m_audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Lune && DataController.LuneActive)
        {
            EnterTheLune();
        }
    }

    public void EnterTheLune()
    {
        DataController.PlayerSavePoint = m_savePoint;
        DataController.PlayerHP = DataController.PlayerMaxHP;
        m_audioSource.PlayOneShot(m_audioClip);
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

    public void Initialized()
    {
        m_isActive = false;
        for (int i = 0; i < m_DivArr.Length; i++)
        {
            m_DivArr[i].gameObject.SetActive(false);
        }
    }
}
