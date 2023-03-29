using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] string m_keyName;
    InventorySystem m_inventorySystem;

    private void Awake()
    {
        m_inventorySystem = GameObject.Find("Inven").GetComponent<InventorySystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            // 플레이어의 인벤토리에 redkey 체크
            PlayerInventorySystem inven = collision.GetComponent<PlayerInventorySystem>();
            for (int i = 0; i < inven.m_index; i++)
            {
                if (inven.m_items[i].m_name == m_keyName)
                {
                    inven.OpenInven();
                    Destroy(m_inventorySystem.transform.GetChild(i).GetChild(0).gameObject);
                    Destroy(inven.m_items[i]);
                    inven.m_items[i] = null;
                    inven.CloseInven();
                    UnlockDoor();
                }
            }
        }
    }

    void UnlockDoor()
    {
        Destroy(gameObject);
    }
}
