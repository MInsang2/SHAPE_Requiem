using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanWindowManager : MonoBehaviour
{
    GameObject[] m_explanWindows = new GameObject[32];

    private void Awake()
    {
        for (int i = 0; i < m_explanWindows.Length; i++)
        {
            m_explanWindows[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (!DataController.IsInvenOpen)
        {
            for (int i = 0; i < m_explanWindows.Length; i++)
            {
                m_explanWindows[i].SetActive(false);
            }
        }
    }
}
