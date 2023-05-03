// 1Â÷ ¸®ÆåÅä¸µ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventorySystem : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    public GameObject playerInventory;
    public Item[] items;
    public int currentIndex;

    private void Start()
    {
        if (playerInventory == null)
        {
            playerInventory = GameObject.Find("Canvas").transform.Find("PlayerUI").Find("Inven").gameObject;
        }

        currentIndex = 0;
        items = new Item[inventorySize];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DataController.IsInvenOpen = !DataController.IsInvenOpen;
        }

        if (DataController.IsInvenOpen)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Item)
        {
            PickUpItem(collision);
        }
    }

    private void PickUpItem(Collider2D collision)
    {
        playerInventory.gameObject.SetActive(true);

        if (collision.GetComponent<Item>() != null)
        {
            items[currentIndex++] = collision.GetComponent<Item>();
            collision.gameObject.SetActive(false);
            collision.transform.parent = transform;
            playerInventory.GetComponent<InventorySystem>().UpdateInventory();
        }
        else
        {
            Debug.Log("collision.GetComponent<Item>() == null");
        }
        playerInventory.gameObject.SetActive(false);
    }

    public void OpenInventory()
    {
        playerInventory.SetActive(true);
    }

    public void CloseInventory()
    {
        playerInventory.SetActive(false);
    }

    public void UseItem(int index)
    {
        playerInventory.GetComponent<InventorySystem>().DeleteItem(index);
        items[index] = null;

        for (int i = index; i < currentIndex; i++)
        {
            items[i] = items[i + 1];
        }

        items[currentIndex] = null;
        currentIndex--;

        playerInventory.GetComponent<InventorySystem>().UpdateInventory();
    }
}
