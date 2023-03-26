using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventorySystem : MonoBehaviour
{
    [SerializeField] int m_invenSize;
    [SerializeField] Item[] m_items;
    [SerializeField] GameObject m_playerInven;
    [SerializeField] Sprite[] m_sprites = new Sprite[100];
    int m_index;

    private void Awake()
    {
        m_index = 0;
        m_items = new Item[m_invenSize];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && m_playerInven.active == false)
        {
            OpenInven();
        }
        else if (Input.GetKeyDown(KeyCode.I) && m_playerInven.active == true)
        {
            CloseInven();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetItem(collision);
    }

    void GetItem(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Item)
        {
            m_playerInven.gameObject.SetActive(true);

            GameObject newObj = new GameObject();
            if (collision.GetComponent<Item>() != null)
            {
                newObj.AddComponent<CanvasRenderer>();
                newObj.AddComponent<Image>().sprite = m_sprites[collision.GetComponent<Item>().m_ID];
                newObj.GetComponent<Image>().color = Color.red;
            }
            else
            {
                Debug.Log("collision.GetComponent<Item>() == null");
            }
            newObj.transform.parent = m_playerInven.transform.GetChild(m_index);
            newObj.transform.position = m_playerInven.transform.GetChild(m_index).transform.position;

            m_items[m_index++] = collision.GetComponent<Item>();
            collision.gameObject.SetActive(false);
            collision.transform.parent = transform;

            m_playerInven.gameObject.SetActive(false);
        }
    }

    void OpenInven()
    {
        m_playerInven.SetActive(true);
    }

    void CloseInven()
    {
        m_playerInven.SetActive(false);
    }

    void AddRedKey()
    {

    }
}
