// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dynamic : MonoBehaviour
{
    protected string m_name; // 에너미의 이름
    protected int damage; // 에너미의 데미지
    protected Collider2D m_collider2D; // 에너미의 콜라이더

    public virtual void TriggerOn()
    {
        
    }

    public string GetName
    {
        get { return m_name; }
    }

    public int GetDamage
    {
        get { return damage; }
    }
}
