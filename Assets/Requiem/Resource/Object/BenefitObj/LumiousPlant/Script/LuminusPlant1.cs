using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuminusPlant1 : MonoBehaviour
{
    Light2D m_light;


    private void Awake()
    {
        m_light = GetComponent<Light2D>();
    }

    void Update()
    {
        SetMoss();
    }

    void SetMoss()
    {
    }
}
