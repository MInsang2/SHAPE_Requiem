using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] PlayerInventorySystem m_playerInvenData;
    [SerializeField] Transform[] m_invenBlock = new Transform[20];

    private void Awake()
    {
        for (int i = 0; i < 20; i++)
        {
            m_invenBlock[i] = transform.GetChild(i);
        }
        gameObject.SetActive(false);
    }

    public void UpdateInven()
    {
        for (int i = 0; i < 20; i++)
        {
            if (m_playerInvenData.m_items[i] != null && m_invenBlock[i].childCount == 0)
            {
                AddItem(m_playerInvenData.m_items[i].m_ID, i);
            }
        }
    }

    void AddItem(int _id, int _index)
    {
        GameObject gameObject = new GameObject("Item");
        gameObject.AddComponent<CanvasRenderer>();
        gameObject.AddComponent<Image>().sprite = DataController.ItemSprites[_id];
        gameObject.GetComponent<Image>().color = Color.red;
        gameObject.transform.parent = m_invenBlock[_index];
        gameObject.transform.position = m_invenBlock[_index].position;
    }

    public void DeleteItem(int _index)
    {
        Destroy(transform.GetChild(_index).GetChild(0).gameObject);
    }
}
