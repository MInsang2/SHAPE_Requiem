using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Swich : MonoBehaviour
{
    public static Action a_Initialized;

    Transform m_parent;
    public bool m_isActive;
    [SerializeField] Sprite m_active;
    [SerializeField] Sprite m_unActive;
    SpriteRenderer m_spriteRenderer;

    private void Awake()
    {
        a_Initialized = () => { Initialized(); };

        m_parent = transform.parent;
        if (m_parent == null)
            Debug.Log("m_parent == null");
        m_isActive = false;
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_spriteRenderer.sprite = m_unActive;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Lune && !m_isActive && DataController.LuneActive)
        {
            m_isActive = true;
            m_spriteRenderer.sprite = m_active;
        }
        else if (collision.gameObject.layer == (int)LayerName.Lune && m_isActive && DataController.LuneActive)
        {
            m_isActive = false;
            m_spriteRenderer.sprite = m_unActive;
        }
    }

    public void Initialized()
    {
        m_isActive = false;
    }
}
