// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Static : MonoBehaviour
{
    protected string m_name; // 적의 이름을 저장하는 변수
    protected int damage; // 적이 입히는 피해량을 저장하는 변수
    protected Collider2D m_collider2D; // 적의 충돌체를 저장하는 변수
    public Vector2 resetPoint; // 리셋 지점의 위치를 저장하는 변수



    // 적이 트리거를 활성화시키는 가상 메소드
    public virtual void TriggerOn()
    {

    }

    // 적의 이름을 반환하는 프로퍼티
    public string GetName
    {
        get { return m_name; }
    }

    // 적이 입히는 피해량을 반환하는 프로퍼티
    public int GetDamage
    {
        get { return damage; }
    }
}
