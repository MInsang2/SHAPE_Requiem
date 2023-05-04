// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneControlledPlatform : MonoBehaviour
{
    [SerializeField] private RuneControllerGPT runeController;
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 destination;

    private Vector2 target;
    private Vector2 origin;
    private bool isRuneAttached = false;

    private float runeMoveTime;

    // 초기 설정 및 변수 값 설정
    private void Start()
    {
        if (runeController == null)
        {
            runeController = PlayerData.PlayerObj.GetComponent<RuneControllerGPT>();
        }

        player = PlayerData.PlayerObj.transform;

        origin = transform.position;
        target = destination;
        runeMoveTime = runeController.moveTime;

        if (player == null) Debug.Log("player == null");
        if (runeController == null) Debug.Log("runeController == null");
    }

    // 룬이 부착되어 있을 경우 플랫폼 이동
    private void Update()
    {
        if (isRuneAttached)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DetachRuneMidway();
            }

            AttachRune();
            MoveToDestination();
        }

        UpdateTarget();
    }

    // 플레이어가 플랫폼에 있을 때 플레이어를 부모로 설정
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            player.parent = transform;
        }
    }

    // 플레이어가 플랫폼에서 벗어날 때 부모 설정 해제
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            player.parent = null;
        }
    }

    // 룬이 플랫폼에 부착되면 움직이기 시작
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Rune && RuneData.RuneActive)
        {
            RuneData.RuneUseControl = false;
            runeController.moveTime = 0.1f;
            isRuneAttached = true;
        }
    }

    // 플랫폼을 목적지로 이동
    private void MoveToDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    // 목적지가 변경되면 플랫폼의 목표 지점 변경
    private void UpdateTarget()
    {
        if ((Vector2)transform.position == origin)
        {
            if (isRuneAttached)
            {
                DetachRuneAtEnd();
            }
            target = destination;
        }

        if ((Vector2)transform.position == destination)
        {
            if (isRuneAttached)
            {
                DetachRuneAtEnd();
            }
            target = origin;
        }
    }

    // 목적지 도달 시 룬 제거 및 조작 가능
    private void DetachRuneAtEnd()
    {
        runeController.moveTime = runeMoveTime;
        RuneData.RuneUseControl = true;
        runeController.target = player.position;
        isRuneAttached = false;
        runeController.isShoot = false;
    }

    // 도중에 룬이 빠질 경우 설정
    private void DetachRuneMidway()
    {
        runeController.moveTime = runeMoveTime;
        RuneData.RuneUseControl = true;
        runeController.target = player.position;
        isRuneAttached = false;
        runeController.isShoot = true;
    }

    // 룬을 플랫폼에 부착
    private void AttachRune()
    {
        runeController.target = transform.position;
    }
}
