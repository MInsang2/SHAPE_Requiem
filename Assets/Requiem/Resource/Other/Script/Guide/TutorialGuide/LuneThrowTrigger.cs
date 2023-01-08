using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuneThrowTrigger : MonoBehaviour
{
    [SerializeField] GameObject m_luneThrowGuide;

    bool m_isActive = false;

    void Start()
    {
        m_luneThrowGuide.SetActive(false);
    }

    void Update()
    {
        if (DataController.PlayerDeathCount == 1)
        {
            m_luneThrowGuide.SetActive(true);
            m_isActive = true;
        }

        if (m_isActive && Input.GetMouseButtonDown(0))
        {
            m_luneThrowGuide.SetActive(false);
            Destroy(m_luneThrowGuide);
            Destroy(this);
        }
    }
}
