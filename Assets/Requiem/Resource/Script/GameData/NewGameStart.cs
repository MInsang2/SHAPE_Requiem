// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewGameStart : MonoBehaviour
{
    [SerializeField] private Transform player; // 플레이어 오브젝트
    [SerializeField] private Transform rune; // 룬 오브젝트

    private Animator playerAnimator; // 플레이어 애니메이터


    private void Start()
    {
        InitializeGame(); // 게임 초기화
    }

    private void InitializeGame()
    {
        player = PlayerData.PlayerObj.transform;
        rune = RuneData.RuneObj.transform;

        PlayerData.PlayerIsMove = false; // 플레이어 이동 불가능
        PlayerData.PlayerIsGetRune = false; // 플레이어 룬 획득 불가능
        playerAnimator = player.GetComponent<Animator>(); // 플레이어 애니메이터 컴포넌트 받아옴
        playerAnimator.SetBool("IsFirstStart", true); // 처음 시작 상태로 설정

        if (player == null) Debug.Log("player == null");
        if (rune == null) Debug.Log("rune == null");
        if (playerAnimator == null) Debug.Log("playerAnimator == null");
    }

    private void Update()
    {
        CheckMoveKeyPress(); // 이동 키 누름 체크
    }

    private void CheckMoveKeyPress()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) // A 또는 D 키가 눌렸을 경우
        {
            playerAnimator.SetBool("IsFirstStart", false); // 첫 시작 애니메이션 종료
            StartCoroutine(EnablePlayerMovement()); // 플레이어 이동 활성화 코루틴 시작
        }
    }

    private IEnumerator EnablePlayerMovement()
    {
        yield return new WaitForSeconds(1f); // 1초 대기

        PlayerData.PlayerIsMove = true; // 플레이어 이동 가능
        Destroy(GetComponent<NewGameStart>());
    }
}
