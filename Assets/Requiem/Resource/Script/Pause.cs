using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject m_pausePanel;
    [SerializeField] GameObject m_optionPanel;
    bool m_isPause;

    private void Start()
    {
        m_isPause = false;
        m_pausePanel.SetActive(false);
        m_optionPanel.SetActive(false);
    }

    void Update()
    {
        InputEscapeKey();
        GamePause();
    }

    void InputEscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_isPause = true;
        }
    }

    void GamePause()
    {
        if (m_isPause)
        {
            Time.timeScale = 0f;
            m_pausePanel.SetActive(m_isPause);
        }
        else
        {
            Time.timeScale = 1f;
            m_pausePanel.SetActive(m_isPause);
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void ContinueButton()
    {
        m_isPause = false;
    }

    public void RestartButton()
    {
        // 게임 재시작
        // 오브젝트들의 리셋 활성화
        //PlayerController.a_Initialized();
        //UpDownPlatform.a_Initialized();
        //RuneStatue.a_Initialized();
        //Swich.a_Initialized();
        //FallingPlatform.a_Initialized();
        //LuneManager.a_Initialized();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("game reset");
    }

    public void OptionButton()
    {
        m_optionPanel.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OptionReturn()
    {
        m_optionPanel.SetActive(false);
    }
}
