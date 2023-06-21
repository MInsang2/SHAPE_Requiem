using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTriggerSub : Trigger_Requiem
{
    [SerializeField] JumpTrigger m_jumpTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            m_jumpTrigger.m_isJump = true;
            m_jumpTrigger.m_isActive = true;
            m_jumpTrigger.m_onTrigger = false;
            m_jumpTrigger.m_jumpGuide.SetActive(false);
        }
        
    }
}
