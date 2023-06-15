using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MoveTrigger : MonoBehaviour
{
    [SerializeField] GameObject m_moveGuide;
    [SerializeField] float m_delayTime;
    bool m_isActive = false;
    float m_currentTime;


    private void Start()
    {
        if (m_moveGuide == null)
        {
            m_moveGuide = DataController.CanvasObj.transform.Find("MoveGuide").gameObject;
        }
        m_moveGuide.SetActive(false);
    }

    private void Update()
    {
        if (!m_isActive)
        {
            GuideMove();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            m_isActive = true;
            m_moveGuide.SetActive(false);
        }
    }

    void GuideMove()
    {
        if (m_currentTime < m_delayTime)
        {
            m_currentTime += Time.deltaTime;
        }
        else
        {
            m_moveGuide.SetActive(true);
            m_isActive = true;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, gameObject.tag);
    }
#endif
}
