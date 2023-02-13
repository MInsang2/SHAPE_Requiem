using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using DG.Tweening;


public class DisapearTrigger : MonoBehaviour
{
    
    [SerializeField] Tilemap m_tileMap;
    [SerializeField] float m_changeTime;

    float m_colorAlpha = 1f;
    Color m_color = new Color(1f,1f,1f,1f);
    bool m_playerIn = false;

    void Start()
    {
    }

    void Update()
    {
        ColorChange();
        m_color = new Color(1f, 1f, 1f, m_colorAlpha);
        m_tileMap.color = m_color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            m_playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            m_playerIn = false;
        }
    }

    void ColorChange()
    {
        if (m_playerIn)
        {
            DOTween.To(() => m_colorAlpha, x => m_colorAlpha = x, 0f, m_changeTime);
        }
        else
        {
            DOTween.To(() => m_colorAlpha, x => m_colorAlpha = x, 1f, m_changeTime);
        }
    }
}
