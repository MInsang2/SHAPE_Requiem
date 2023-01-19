using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuminusPlant1 : MonoBehaviour
{
    Light2D m_light;
    CircleCollider2D m_lightArea;


    private void Awake()
    {
        m_light = GetComponent<Light2D>();
        m_lightArea = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        SetMoss();
        m_lightArea.radius = m_light.pointLightOuterRadius * 2;
    }

    void SetMoss()
    {
    }
}
