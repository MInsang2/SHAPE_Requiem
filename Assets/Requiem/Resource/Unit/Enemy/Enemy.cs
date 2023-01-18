using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected string m_name;
    protected int m_damage;

    public virtual void TriggerOn()
    {
        
    }

    public string GetName
    {
        get { return m_name; }
    }

    public int GetDamage
    {
        get { return m_damage; }
    }
}
