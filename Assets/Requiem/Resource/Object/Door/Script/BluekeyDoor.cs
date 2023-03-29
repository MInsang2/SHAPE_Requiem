using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluekeyDoor : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            PlayerInventorySystem inven = collision.GetComponent<PlayerInventorySystem>();
            inven.OpenInven();

            for (int i = 0; i < inven.m_index; i++)
            {
                if (inven.m_items[i].m_ID == 1)
                {
                    inven.UseItem(i);
                    inven.CloseInven();
                    Destroy(gameObject);
                    break;
                }
            }
            inven.m_playerInven.GetComponent<InventorySystem>().UpdateInven();
            inven.CloseInven();
        }
    }
}
