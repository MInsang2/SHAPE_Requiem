// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private int keyID; // 키 ID
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private float invokeTime;
    [SerializeField] public bool isOpened;
    [SerializeField] private SpriteRenderer openedSprite;
    [SerializeField] private LightsManager lightsManager;

    private AudioSource audioSource;


    private void Start()
    {
        openedSprite = transform.Find("Lit").GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null) Debug.Log("audioSource == null");
        openedSprite.gameObject.SetActive(true);
    }

    private void Update()
    {
        DoorStateChange();
    }

    // 트리거에 다른 오브젝트가 있을 때 처리하는 함수
    private void OnTriggerStay2D(Collider2D collision)
    {
        //상호작용 유도 UI 배치

        if (IsPlayer(collision) && Input.GetKeyDown(KeyCode.F))
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
        inven.OpenInventory();

        for (int i = 0; i < inven.currentIndex; i++)
        {
            if (HasKey(inven, i))
            {
                UseKeyAndActiveDoor(inven, i);
                break;
            }
        }
        UpdateAndCloseInventory(inven);
    }

    // 인벤토리에 키가 있는지 확인하는 함수
    private bool HasKey(PlayerInventorySystem inven, int index)
    {
        return inven.items[index].m_ID == keyID;
    }

    // 키를 사용하고 문을 작동하는 함수
    private void UseKeyAndActiveDoor(PlayerInventorySystem inven, int index)
    {
        inven.UseItem(index);
        inven.CloseInventory();
        isOpened = true;
        audioSource.PlayOneShot(doorSound);
    }

    // 인벤토리를 업데이트하고 닫는 함수
    private void UpdateAndCloseInventory(PlayerInventorySystem inven)
    {
        inven.playerInventory.GetComponent<InventorySystem>().UpdateInventory();
        inven.CloseInventory();
    }

    void DoorStateChange()
    {
        if (isOpened)
        {
            openedSprite.DOColor(new Color(255f, 255f, 255f, 255f), lightsManager.turnOnTime);
            lightsManager.turnOffValue = false;
        }
        else
        {
            openedSprite.DOColor(new Color(255f, 255f, 255f, 0f), lightsManager.turnOffTime);
            lightsManager.turnOffValue = true;
        }
    }

    void DestroyDoor()
    {
        Destroy(gameObject);
    }
}
