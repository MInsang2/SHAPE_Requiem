// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseOver;
    public GameObject explanWindow;
    private PlayerInventorySystem playerInven;
    private int index;

    // 초기화
    private void Start()
    {
        InitializeVariables();
    }

    // 변수 초기화
    private void InitializeVariables()
    {
        mouseOver = false;
        playerInven = PlayerData.PlayerObj.GetComponent<PlayerInventorySystem>();
        index = transform.GetSiblingIndex();
        InitializeExplanWindow();
    }

    // 설명 창 초기화
    private void InitializeExplanWindow()
    {
        explanWindow = GameObject.Find("ExplanWindowEvery").transform.GetChild(index).gameObject;
        explanWindow.SetActive(false);
    }

    // 설명 창 업데이트
    private void FixedUpdate()
    {
        if (DataController.IsInvenOpen)
        {
            UpdateExplanWindow();
        }
        else
        {
            explanWindow.SetActive(false);
        }
    }

    // 설명 창 상태에 따른 업데이트
    private void UpdateExplanWindow()
    {
        if (mouseOver && playerInven.m_items[index] != null)
        {
            explanWindow.SetActive(true);
            ChangeExplanWindow(playerInven.m_items[index]);
        }
        else
        {
            explanWindow.SetActive(false);
        }
    }

    // 마우스 오버 시
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    // 마우스가 벗어날 때
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }

    // 설명 창에 텍스트 변경
    private void ChangeExplanWindow(Item item)
    {
        TextMeshProUGUI TMPro = explanWindow.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TMPro.text = item.m_explanation;
    }
}
