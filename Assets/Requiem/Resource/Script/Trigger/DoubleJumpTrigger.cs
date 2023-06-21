using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleJumpTrigger : Trigger_Requiem
{
    // 더블 점프 서브를 만들어서 적용시키면 댐
    // 트리거에 접촉하면 시간 재생
    // 플레이어 상부에 트리거 만들어서 접촉 시 트리거 해제
    [SerializeField] public GameObject m_doubleJumpGuide;
    public float m_currentTime = 0f;
    public float m_delayTime = 5f;
    public bool m_isActive = false;
    public bool m_isJump = false;
    public bool m_onTrigger = false;

    private void Start()
    {
        if (m_doubleJumpGuide == null)
        {
            m_doubleJumpGuide = DataController.CanvasObj.transform.Find("DoubleJumpGuide").gameObject;
        }
        m_doubleJumpGuide.SetActive(false);
    }

    private void Update()
    {
        if (m_onTrigger)
        {
            GuideJump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            if (!m_isJump)
            {
                m_onTrigger = true;
            }
        }
    }

    public void GuideJump()
    {
        if (m_currentTime < m_delayTime)
        {
            m_currentTime += Time.deltaTime;
        }
        else
        {
            m_doubleJumpGuide.SetActive(true);
            m_isActive = true;
        }
    }
}
