using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] Transform[] m_invenBlock = new Transform[32];
    [SerializeField] PlayerInventorySystem m_playerInvenData;

    private void Start()
    {
        m_playerInvenData = PlayerData.PlayerObj.GetComponent<PlayerInventorySystem>();

        for (int i = 0; i < 32; i++)
        {
            m_invenBlock[i] = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i);
        }
        gameObject.SetActive(false);
    }

    public void UpdateInven()
    {
        for (int i = 0; i < 32; i++)
        {
            DeleteItem(i);
        }

        for (int i = 0; i < 32; i++)
        {
            if (m_playerInvenData.m_items[i] != null)
            {
                AddItem(m_playerInvenData.m_items[i].m_ID, i);
            }
        }
    }

    void AddItem(int _id, int _index)
    {
        m_invenBlock[_index].GetChild(0).GetComponent<Image>().sprite =
            DataController.ItemSprites[_id];

        m_invenBlock[_index].GetChild(0).GetComponent<Image>().color =
            m_playerInvenData.m_items[_index].GetComponent<SpriteRenderer>().color;
    }

    public void DeleteItem(int _index)
    {
        m_invenBlock[_index].GetChild(0).GetComponent<Image>().sprite = null;
        m_invenBlock[_index].GetChild(0).GetComponent<Image>().color = Color.white;
    }
}
