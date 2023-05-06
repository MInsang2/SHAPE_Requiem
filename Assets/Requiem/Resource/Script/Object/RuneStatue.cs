// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;

public class RuneStatue : MonoBehaviour
{
    [SerializeField] private Vector2 savePoint; // 세이브 포인트
    [SerializeField] public bool isActive; // 동작 했는가 여부
    [SerializeField] private AudioClip audioClip; // 동작 시 재생 소리

    private Animator animator; // 자신의 애니매이터
    private AudioSource audioSource; // 자신의 오디오 소스
    private AudioSource audioSourceActive; // 동작 시의 오디오 소스
    private bool isPlay; // 재생 되었는지 여부

    // 컴포넌트 초기화와 값 설정을 위한 Awake 함수
    private void Start()
    {
        InitializeComponents();
        InitializeValues();
    }

    // 컴포넌트 초기화를 위한 함수
    private void InitializeComponents()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSourceActive = transform.Find("Sound").GetComponent<AudioSource>();

        if (animator == null) Debug.Log("m_animator == null");
        if (audioSource == null) Debug.Log("m_audioSource == null");
    }

    // 값 초기화를 위한 함수
    private void InitializeValues()
    {
        if (savePoint == Vector2.zero)
        {
            savePoint = transform.position;
        }

        isActive = false;
        isPlay = false;
    }

    // 트리거에 다른 오브젝트가 있을 때 처리하는 함수
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Rune && RuneData.RuneActive)
        {
            EnterTheRune();
        }

        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            UpdatePlayerData();
        }
    }

    // 룬 입장 처리를 위한 함수
    public void EnterTheRune()
    {
        if (!isActive)
        {
            UpdatePlayerData();
            ActivateRuneStatue();
        }
    }

    // 플레이어 데이터 업데이트를 위한 함수
    private void UpdatePlayerData()
    {
        PlayerData.PlayerSavePoint = savePoint;
        PlayerData.PlayerHP = PlayerData.PlayerMaxHP;
    }

    // 룬 석상 상태 활성화를 위한 함수
    private void ActivateRuneStatue()
    {
        animator.SetBool("IsActive", true);
        PlayAudioClip();
    }

    // 오디오 클립 재생을 위한 함수
    private void PlayAudioClip()
    {
        if (!isPlay)
        {
            audioSourceActive.PlayOneShot(audioClip);
            isPlay = true;
        }
    }

    // 룬 석상 초기화를 위한 함수
    public void Initialized()
    {
        isActive = false;
    }
}
