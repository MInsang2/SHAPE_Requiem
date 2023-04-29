// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : Item
{
    void Start()
    {
        gameObject.layer = (int)LayerName.Item; // 아이템 레이어 설정
        m_collider = GetComponent<Collider2D>(); // 자신의 콜라이더
        m_animator = GetComponent<Animator>(); // 자신의 애니매이터

        if (m_collider == null) Debug.Log("m_collider == null");
        if (m_animator == null) Debug.Log("m_animator == null");
    }

    void Update()
    {
        
    }
}
