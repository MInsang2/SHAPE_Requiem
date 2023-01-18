using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] Transform m_platformPos;
    public string m_name;
    Vector2 m_posOrigin;


    private void Start()
    {
        m_posOrigin = m_platformPos.position;
    }

    public void Initialized()
    {
        m_platformPos.position = m_posOrigin;
    }    
}
