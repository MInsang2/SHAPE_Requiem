using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class OjakgyoPlatform : MonoBehaviour
{
    [SerializeField] float m_desX;
    [SerializeField] float m_desY;
    [SerializeField] float m_moveTime;
    [SerializeField] float m_timeLaps;
    [SerializeField] AudioSource m_audioSource;

    Vector2 m_initialPos;
    Vector2 m_destyPos;
    float m_delayTime;
    bool m_isActive;

    private void Awake()
    {
        m_audioSource.gameObject.SetActive(false);
        m_initialPos = transform.position;
        m_destyPos = new Vector2(m_desX, m_desY);
        m_delayTime = 0f;
    }

    void Update()
    {
        MovePlatform();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == (int)LayerName.Player)
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            collision.transform.parent = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Lune && DataController.LuneActive)
        {
            m_isActive = true;
        }
    }

    void MovePlatform()
    {
        if (m_delayTime <= m_timeLaps && m_isActive)
        {
            transform.DOMove(m_destyPos, m_moveTime);
            m_audioSource.gameObject.SetActive(true);
            m_delayTime += Time.deltaTime;
        }
        else if (m_delayTime > m_timeLaps)
        {
            transform.DOMove(m_initialPos, m_moveTime);
            m_audioSource.gameObject.SetActive(false);
            m_delayTime = 0f;
            m_isActive = false;
        }
    }
}
