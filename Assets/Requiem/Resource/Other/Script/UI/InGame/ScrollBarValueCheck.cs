using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollBarValueCheck : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_value;
    [SerializeField] Scrollbar m_scrollBar;

    void Update()
    {
        m_value.text = m_scrollBar.value.ToString();
    }
}
