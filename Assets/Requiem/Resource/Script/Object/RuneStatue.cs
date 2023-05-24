// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;
using DG.Tweening;

public class RuneStatue : MonoBehaviour
{
    [SerializeField] private Vector2 savePoint; // 세이브 포인트
    [SerializeField] public bool isActive; // 동작 했는가 여부
    [SerializeField] private AudioClip audioClip; // 동작 시 재생 소리

    private Animator animator; // 자신의 애니매이터
    private AudioSource audioSource; // 자신의 오디오 소스
    private AudioSource audioSourceActive; // 동작 시의 오디오 소스
    private ParticleSystem activeEffect;
    private Light2D activeLight;
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
        activeEffect = transform.Find("ActiveEffect").GetComponent<ParticleSystem>();
        activeLight = transform.Find("ActiveLight").GetComponent<Light2D>();
        activeLight.shapeLightFalloffSize = 0f;

        activeEffect.gameObject.SetActive(false);
        activeLight.gameObject.SetActive(false);

        if (animator == null) Debug.Log("animator == null");
        if (audioSource == null) Debug.Log("audioSource == null");
        if (activeEffect == null) Debug.Log("activeEffect == null");
        if (activeLight == null) Debug.Log("activeLight == null");
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
        Invoke("ActivateEffect", 5f);
        
        PlayAudioClip();
    }

    private void ActivateEffect()
    {
        activeEffect.gameObject.SetActive(true);
        activeLight.gameObject.SetActive(true);
        DOTween.To(() => activeLight.shapeLightFalloffSize, x => activeLight.shapeLightFalloffSize = x, 5, 10);
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
