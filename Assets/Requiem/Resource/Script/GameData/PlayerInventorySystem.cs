using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventorySystem : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] AudioClip[] inventorySounds;
    public GameObject playerInventory;
    public Item[] items;
    public int currentIndex;

    AudioSource audioSource;

    private void Start()
    {
        if (playerInventory == null)
        {
            playerInventory = GameObject.Find("Canvas").transform.Find("PlayerUI").Find("Inven").gameObject;
        }

        audioSource = GetComponent<AudioSource>();

        currentIndex = 0;
        items = new Item[inventorySize];

        if (audioSource == null) Debug.Log("audioSource == null");
        if (playerInventory == null) Debug.Log("playerInventory == null");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetIsInvenOpen(!DataController.IsInvenOpen);
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
            PlayAudioClip(2);
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

    private void PlayAudioClip(int index)
    {
        if (audioSource != null && inventorySounds.Length > index && inventorySounds[index] != null)
        {
            audioSource.clip = inventorySounds[index];
            audioSource.Play();
        }
    }

    private void SetIsInvenOpen(bool isOpen)
    {
        if (DataController.IsInvenOpen != isOpen)
        {
            DataController.IsInvenOpen = isOpen;
            PlayAudioClip(isOpen ? 0 : 1);
        }
    }
}
