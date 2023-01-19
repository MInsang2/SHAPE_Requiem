using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LuminusLamp : MonoBehaviour
{
    [SerializeField] Swich m_swich;
    [SerializeField] Light2D m_light;
    [SerializeField] float m_outerRadius;
    CircleCollider2D m_lightArea;

    private void Start()
    {
        m_lightArea = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (m_swich.m_isActive)
        {
            m_light.pointLightOuterRadius = m_outerRadius;
            DOTween.To(() => m_light.pointLightOuterRadius, x => m_light.pointLightOuterRadius = x, m_outerRadius, 5f);
            m_lightArea.enabled = true;
        }
        else
        {
            DOTween.To(() => m_light.pointLightOuterRadius, x => m_light.pointLightOuterRadius = x, 0f, 5f);
            m_lightArea.enabled = false;
        }
    }
}
