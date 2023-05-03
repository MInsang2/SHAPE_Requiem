// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private Transform[] inventoryBlocks = new Transform[32]; // 인벤토리 블록의 Transform 배열
    [SerializeField] private PlayerInventorySystem playerInventoryData; // 플레이어 인벤토리 데이터 참조


    private void Start()
    {
        InitializeVariables(); // 변수 설정
        gameObject.SetActive(false); // 시작 시 인벤토리 숨기기
    }

    // 변수 초기화
    private void InitializeVariables()
    {
        // 플레이어의 인벤토리 시스템 컴포넌트 가져오기
        playerInventoryData = PlayerData.PlayerObj.GetComponent<PlayerInventorySystem>();

        // 인벤토리 블록 할당
        for (int i = 0; i < 32; i++)
        {
            inventoryBlocks[i] = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i);
        }
    }

    // 인벤토리 업데이트
    public void UpdateInventory()
    {
        ClearAllItems(); // 모든 아이템 제거

        for (int i = 0; i < 32; i++)
        {
            if (playerInventoryData.items[i] != null)
            {
                AddItem(playerInventoryData.items[i].m_ID, i); // 아이템 추가
            }
        }
    }

    // 모든 아이템 지우기
    private void ClearAllItems()
    {
        for (int i = 0; i < 32; i++)
        {
            DeleteItem(i);
        }
    }

    // 아이템 추가
    private void AddItem(int id, int index)
    {
        inventoryBlocks[index].GetChild(0).GetComponent<Image>().sprite =
            DataController.ItemSprites[id];

        inventoryBlocks[index].GetChild(0).GetComponent<Image>().color =
            playerInventoryData.items[index].GetComponent<SpriteRenderer>().color;
    }

    // 아이템 삭제
    public void DeleteItem(int index)
    {
        inventoryBlocks[index].GetChild(0).GetComponent<Image>().sprite = null;
        inventoryBlocks[index].GetChild(0).GetComponent<Image>().color = Color.white;
    }
}
