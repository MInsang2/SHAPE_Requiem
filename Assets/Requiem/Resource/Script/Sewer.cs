using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sewer : MonoBehaviour
{
    public bool m_SewerIsOpen;
    public GameObject m_Water;

    void Update()
    {
        if (m_SewerIsOpen)
        {
            m_Water.SetActive(true);
        }
        else
        {
            m_Water.SetActive(false);
        }
    }
}
