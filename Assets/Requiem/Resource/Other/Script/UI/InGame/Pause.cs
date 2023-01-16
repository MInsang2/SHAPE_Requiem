using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] bool m_isPause;
    [SerializeField] GameObject m_pausePanel;


    private void Start()
    {
        m_isPause = false;
        m_pausePanel.SetActive(false);
    }

    void Update()
    {
        EscChecker();
        UI_Update();
    }

    void EscChecker()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!m_isPause)
                m_isPause = true;
        }
    }

    void UI_Update()
    {
        TimeStop();
        OpenPanel();
    }

    void TimeStop()
    {
        if (m_isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    void OpenPanel()
    {
        if (m_isPause)
        {
            m_pausePanel.SetActive(m_isPause);
        }
        else
        {
            m_pausePanel.SetActive(m_isPause);
        }
    }

    public void ContinueButton()
    {
        // 다시 게임 진행
        m_isPause = false;
    }

    public void RestartButton()
    {
        // 씬 재시작
    }

    public void OptionButton()
    {
        // 옵션 창 오픈
    }

    public void QuitButton()
    {
        // 게임 종료
        Application.Quit();
    }
}
