// 1�� �����丵

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private int keyID; // Ű ID
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private float invokeTime;

    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null) Debug.Log("audioSource == null");
    }

    // Ʈ���ſ� �ٸ� ������Ʈ�� ���� �� ó���ϴ� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer(collision))
        {
            PlayerInventorySystem inven = GetPlayerInventorySystem(collision);
            OpenAndSearchInventory(inven);
        }
    }

    // �÷��̾����� Ȯ���ϴ� �Լ�
    private bool IsPlayer(Collider2D collision)
    {
        return collision.gameObject.layer == (int)LayerName.Player;
    }

    // �÷��̾� �κ��丮 �ý����� �������� �Լ�
    private PlayerInventorySystem GetPlayerInventorySystem(Collider2D collision)
    {
        return collision.GetComponent<PlayerInventorySystem>();
    }

    // �κ��丮�� ���� Ű�� ã�� �Լ�
    private void OpenAndSearchInventory(PlayerInventorySystem inven)
    {
        inven.OpenInventory();

        for (int i = 0; i < inven.currentIndex; i++)
        {
            if (HasKey(inven, i))
            {
                UseKeyAndDestroyDoor(inven, i);
                break;
            }
        }
        UpdateAndCloseInventory(inven);
    }

    // �κ��丮�� Ű�� �ִ��� Ȯ���ϴ� �Լ�
    private bool HasKey(PlayerInventorySystem inven, int index)
    {
        return inven.items[index].m_ID == keyID;
    }

    // Ű�� ����ϰ� ���� �����ϴ� �Լ�
    private void UseKeyAndDestroyDoor(PlayerInventorySystem inven, int index)
    {

        inven.UseItem(index);
        inven.CloseInventory();
        audioSource.PlayOneShot(doorSound);
        Invoke("DestroyDoor", invokeTime);
    }

    // �κ��丮�� ������Ʈ�ϰ� �ݴ� �Լ�
    private void UpdateAndCloseInventory(PlayerInventorySystem inven)
    {
        inven.playerInventory.GetComponent<InventorySystem>().UpdateInventory();
        inven.CloseInventory();
    }

    void DestroyDoor()
    {
        Destroy(gameObject);
    }
}