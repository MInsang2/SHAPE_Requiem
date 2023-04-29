// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private int keyID; // 키 ID

    // 트리거에 다른 오브젝트가 있을 때 처리하는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer(collision))
        {
            PlayerInventorySystem inven = GetPlayerInventorySystem(collision);
            OpenAndSearchInventory(inven);
        }
    }

    // 플레이어인지 확인하는 함수
    private bool IsPlayer(Collider2D collision)
    {
        return collision.gameObject.layer == (int)LayerName.Player;
    }

    // 플레이어 인벤토리 시스템을 가져오는 함수
    private PlayerInventorySystem GetPlayerInventorySystem(Collider2D collision)
    {
        return collision.GetComponent<PlayerInventorySystem>();
    }

    // 인벤토리를 열고 키를 찾는 함수
    private void OpenAndSearchInventory(PlayerInventorySystem inven)
    {
        inven.OpenInven();

        for (int i = 0; i < inven.m_index; i++)
        {
            if (HasKey(inven, i))
            {
                UseKeyAndDestroyDoor(inven, i);
                break;
            }
        }
        UpdateAndCloseInventory(inven);
    }

    // 인벤토리에 키가 있는지 확인하는 함수
    private bool HasKey(PlayerInventorySystem inven, int index)
    {
        return inven.m_items[index].m_ID == keyID;
    }

    // 키를 사용하고 문을 제거하는 함수
    private void UseKeyAndDestroyDoor(PlayerInventorySystem inven, int index)
    {
        inven.UseItem(index);
        inven.CloseInven();
        Destroy(gameObject);
    }

    // 인벤토리를 업데이트하고 닫는 함수
    private void UpdateAndCloseInventory(PlayerInventorySystem inven)
    {
        inven.m_playerInven.GetComponent<InventorySystem>().UpdateInventory();
        inven.CloseInven();
    }
}
