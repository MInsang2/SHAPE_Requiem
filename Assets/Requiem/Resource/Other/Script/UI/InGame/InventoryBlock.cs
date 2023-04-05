using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool m_mouseOver;
    public GameObject m_explanWindow;
    public PlayerInventorySystem m_playerInven;
    int m_index;


    private void Awake()
    {
        m_mouseOver = false;
        m_playerInven = GameObject.Find("player").GetComponent<PlayerInventorySystem>();
        m_index = transform.GetSiblingIndex();
        m_explanWindow = GameObject.Find("ExplanWindowEvery").transform.GetChild(m_index).gameObject;
        m_explanWindow.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (m_mouseOver)
        {
            if (m_playerInven.m_items[m_index] != null)
            {
                m_explanWindow.SetActive(true);
                ChangeExplanWindow(m_playerInven.m_items[m_index]);
            }
        }
        else
        {
            m_explanWindow.SetActive(false);
        }

        if (!DataController.IsInvenOpen)
        {
            m_explanWindow.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_mouseOver = false;
    }

    void ChangeExplanWindow(Item _item)
    {
        TextMeshProUGUI TMPro = m_explanWindow.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TMPro.text = _item.m_explanation;
    }
}
