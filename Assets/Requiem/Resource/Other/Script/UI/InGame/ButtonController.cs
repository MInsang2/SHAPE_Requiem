using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject m_PlayerInven;

    public void CloseInven()
    {
        m_PlayerInven.SetActive(false);
    }
}
