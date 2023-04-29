using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LuminusPlant3 : MonoBehaviour
{
    [SerializeField] float m_maxRadius;
    [SerializeField] float m_minRadius;
    [SerializeField] float m_lightTime;

    Light2D m_light;
    bool m_isMax;
    float m_delayTime;


    private void Awake()
    {
        m_light = GetComponent<Light2D>();
        m_isMax = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        LuminusBlink();
    }

    void LuminusBlink()
    {
        if (m_light.pointLightInnerRadius < m_maxRadius && !m_isMax)
        {
            DOTween.To(() => m_light.pointLightInnerRadius, x => m_light.pointLightInnerRadius = x, m_maxRadius, m_lightTime);
            m_delayTime += Time.deltaTime;
            if (m_delayTime >= m_lightTime)
            {
                m_delayTime = 0f;
                m_isMax = true;
            }
        }
        
        if (m_light.pointLightInnerRadius >= m_minRadius && m_isMax)
        {
            DOTween.To(() => m_light.pointLightInnerRadius, x => m_light.pointLightInnerRadius = x, m_minRadius, m_lightTime);
            m_delayTime += Time.deltaTime;
            if (m_delayTime >= m_lightTime)
            {
                m_delayTime = 0f;
                m_isMax = false;
            }
        }
    }
}
