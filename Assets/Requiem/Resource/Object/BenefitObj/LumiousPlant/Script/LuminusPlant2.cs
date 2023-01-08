using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LuminusPlant2 : MonoBehaviour
{
    [SerializeField] float m_maxRadius;
    [SerializeField] float m_minRadius;
    [SerializeField] float m_lightTime;
    [SerializeField] float m_timeLaps;

    Transform m_object;
    Light2D m_light;

    float m_delayTime;
    bool m_isBright;


    private void Awake()
    {
        m_light = GetComponent<Light2D>();
        m_delayTime = 0f;
        m_isBright = false;
    }

    void Update()
    {
        if (m_isBright)
        {
            m_delayTime += Time.deltaTime;
        }
        if (m_delayTime > m_timeLaps)
        {
            DOTween.To(() => m_light.pointLightOuterRadius, x => m_light.pointLightOuterRadius = x, m_minRadius, m_lightTime);
            m_delayTime = 0f;
            m_isBright = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Lune && m_light.pointLightOuterRadius < m_maxRadius)
        {
            DOTween.To(() => m_light.pointLightOuterRadius, x => m_light.pointLightOuterRadius = x, m_maxRadius, m_lightTime);
            m_isBright = true;
        }
        

    }
}
