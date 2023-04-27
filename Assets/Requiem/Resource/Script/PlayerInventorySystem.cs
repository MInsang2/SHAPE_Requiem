using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventorySystem : MonoBehaviour
{
    [SerializeField] int m_invenSize;
    public GameObject m_playerInven;
    public Item[] m_items;
    public int m_index;




    private void Start()
    {
        if (m_playerInven == null)
        {
            m_playerInven = GameObject.Find("Canvas").transform.Find("PlayerUI").Find("Inven").gameObject;
        }

        m_index = 0;
        m_items = new Item[m_invenSize];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DataController.IsInvenOpen = !DataController.IsInvenOpen;
        }

        if (DataController.IsInvenOpen)
        {
            OpenInven();
        }
        else
        {
            CloseInven();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Item)
        {
            GetItem(collision);
        }
    }

    void GetItem(Collider2D collision)
    {
        m_playerInven.gameObject.SetActive(true);

        if (collision.GetComponent<Item>() != null)
        {
            m_items[m_index++] = collision.GetComponent<Item>();
            collision.gameObject.SetActive(false);
            collision.transform.parent = transform;
            m_playerInven.GetComponent<InventorySystem>().UpdateInven();
        }
        else
        {
            Debug.Log("collision.GetComponent<Item>() == null");
        }
        m_playerInven.gameObject.SetActive(false);
    }

    public void OpenInven()
    {
        m_playerInven.SetActive(true);
    }

    public void CloseInven()
    {
        m_playerInven.SetActive(false);
    }

    public void UseItem(int _index)
    {
        m_playerInven.GetComponent<InventorySystem>().DeleteItem(_index);
        m_items[_index] = null;

        for (int i = _index; i < m_index; i++)
        {
            m_items[i] = m_items[i + 1];
        }

        m_items[m_index] = null;
        m_index--;

        m_playerInven.GetComponent<InventorySystem>().UpdateInven();
    }
}
