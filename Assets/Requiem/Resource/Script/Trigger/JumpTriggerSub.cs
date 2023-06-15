using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class JumpTriggerSub : MonoBehaviour
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, gameObject.tag);
    }
#endif
}
