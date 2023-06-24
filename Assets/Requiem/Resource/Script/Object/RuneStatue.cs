// 1�� �����丵

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;
using DG.Tweening;

public class RuneStatue : MonoBehaviour
{
    [SerializeField] private Vector2 savePoint; // ���̺� ����Ʈ
    [SerializeField] private float lightRadius;
    [SerializeField] private float lightPower;
    [SerializeField] private float lightPowerDownTime;
    [SerializeField] private float lightLessPower;
    [SerializeField] private float effectDelay = 5f; // ȿ�� ������ �ð�
    [SerializeField] public bool isActive; // ���� �ߴ°� ����
    [SerializeField] private AudioClip audioClip; // ���� �� ��� �Ҹ�
    [SerializeField] LightsManager[] lightsManagers;
    [SerializeField] public float runeChargePower = 50f;

    private Animator animator; // �ڽ��� �ִϸ�����
    private AudioSource audioSource; // �ڽ��� ����� �ҽ�
    private AudioSource audioSourceActive; // ���� ���� ����� �ҽ�
    private ParticleSystem activeEffect;
    private Light2D activeLight;
    private bool isPlay; // ��� �Ǿ����� ����

    // ������Ʈ �ʱ�ȭ�� �� ������ ���� Awake �Լ�
    private void Start()
    {
        InitializeComponents();
        InitializeValues();
    }

    // ������Ʈ �ʱ�ȭ�� ���� �Լ�
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

    // �� �ʱ�ȭ�� ���� �Լ�
    private void InitializeValues()
    {
        if (savePoint == Vector2.zero)
        {
            savePoint = transform.position;
        }

        isActive = false;
        isPlay = false;
    }

    // Ʈ���ſ� �ٸ� ������Ʈ�� ���� �� ó���ϴ� �Լ�
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive) // �̹� Ȱ��ȭ�� ��� �Լ��� ����
        {
            return;
        }

        if (collision.gameObject.layer == (int)LayerName.Rune && RuneData.RuneActive)
        {
            EnterTheRune();
        }

        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            UpdatePlayerData();
        }
    }

    // �� ���� ó���� ���� �Լ�
    public void EnterTheRune()
    {
        if (!isActive || RuneData.RuneBattery <= 0)
        {
            UpdatePlayerData();
            ActivateRuneStatue();
        }
    }

    // �÷��̾� ������ ������Ʈ�� ���� �Լ�
    private void UpdatePlayerData()
    {
        PlayerData.PlayerSavePoint = savePoint;
        PlayerData.PlayerHP = PlayerData.PlayerMaxHP;
    }

    private bool hasTriggered = false;
    // �� ���� ���� Ȱ��ȭ�� ���� �Լ�
    private void ActivateRuneStatue()
    {
        if (!hasTriggered)
        {
            animator.SetTrigger("IsActive");
            hasTriggered = true;
        }
        PlayerData.PlayerObj.GetComponent<RuneControllerGPT>().RunePowerBack();
        Invoke("ActivateEffect", effectDelay);
        Invoke("TurnOnLights", effectDelay);
        DOTween.To(() => RuneData.RuneBattery, x => RuneData.RuneBattery = x, RuneData.RuneBatteryInitValue, 5f);
        PlayAudioClip();
    }

    private void ActivateEffect()
    {
        activeEffect.gameObject.SetActive(true);
        activeLight.gameObject.SetActive(true);
        DOTween.To(() => activeLight.shapeLightFalloffSize, x => activeLight.shapeLightFalloffSize = x, 5, 10);
    }

    // ����� Ŭ�� ����� ���� �Լ�
    private void PlayAudioClip()
    {
        if (!isPlay)
        {
            audioSourceActive.PlayOneShot(audioClip);
            isPlay = true;
        }
    }

    // �� ���� �ʱ�ȭ�� ���� �Լ�
    public void Initialized()
    {
        isActive = false;
        hasTriggered = false; // Reset the trigger flag
    }

    // ����� �� ��ü�� Ȱ��ȭ
    void TurnOnLights()
    {
        for (int i = 0; i < lightsManagers.Length; i++)
        {
            lightsManagers[i].windowLightType = WindowLightType.FULL;

            lightsManagers[i].turnOffValue = false;
        }
    }
}
