using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;

public class RuneManager : MonoBehaviour
{

    [SerializeField] Transform m_statue;
    [SerializeField] float m_moveTime = 3f;
    [SerializeField] float m_rotationSpeed = 10f;
    [SerializeField] bool m_isStatueInteraction = false;

    RuneControllerGPT m_runeControl;
    Light2D m_luneLight;
    Vector2 m_origin;


    private void Start()
    {
        m_runeControl = GameObject.Find("Player").GetComponent<RuneControllerGPT>();
        m_luneLight = GameObject.Find("Rune").GetComponent<Light2D>();
        m_origin = transform.position;
    }

    private void Update()
    {
        if (m_isStatueInteraction)
        {
            if (!m_statue.GetComponent<RuneStatue>().isActive)
            {
                StatueInteraction(m_statue);
            }
        }
    }

    /// <summary>
    /// ���� Ư�� �ݶ��̴� ����  �ӹ� �� �ߵ�(Ʈ����),
    /// ���� �����ϰ� �ȴ�.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case (int)LayerName.Platform:
            case (int)LayerName.Wall:
            case (int)LayerName.RiskFactor:
                m_runeControl.RuneStop();
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<RuneStatue>() != null)
        {
            if (RuneData.RuneActive || RuneData.RuneBattery <= 0)
            {
                if (RuneData.RuneBattery <= 0)
                {
                    collision.gameObject.GetComponent<RuneStatue>().Initialized();
                }
                m_statue = collision.transform;
                m_isStatueInteraction = true;
            }
        }

        if (collision.gameObject.GetComponent<RuneStatue>() != null)
        {
            PlayerData.PlayerObj.GetComponent<RuneControllerGPT>().isCharge = true;

            if (RuneData.RuneBattery < RuneData.RuneBatteryInitValue)
            {
                RuneData.RuneBattery += collision.gameObject.GetComponent<RuneStatue>().runeChargePower * Time.deltaTime;
            }
            else
            {
                RuneData.RuneBattery = RuneData.RuneBatteryInitValue;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<RuneStatue>() != null)
        {
            PlayerData.PlayerObj.GetComponent<RuneControllerGPT>().isCharge = false;
        }
    }

    /// <summary>
    /// ���� �ӹ� ��� �ߵ�
    /// </summary>
    public void EnterWater()
    {
        RuneData.RuneTouchWater = true;
        RuneData.RuneActive = false;
        RuneData.RuneOnWater = true;
        RuneData.RuneLightArea.enabled = false;
    }

    /// <summary>
    /// ������ ���� ��� �ߵ�
    /// </summary>
    public void ExitWater()
    {
        RuneData.RuneOnWater = false;
    }

    /// <summary>
    /// �� �þ� ������ �����Ѵ�.
    /// </summary>
    void ChangeLightArea()
    {
        RuneData.RuneLightArea.radius = m_luneLight.pointLightOuterRadius;
    }

    public void StatueInteraction(Transform _target)
    {
        m_runeControl.target = _target.position;
        RuneData.RuneUseControl = false;
        transform.Rotate(Vector3.back * m_rotationSpeed);
        transform.DOMove(m_runeControl.target, m_moveTime);
        StartCoroutine("StatueInteractionDelay");
    }

    IEnumerator StatueInteractionDelay()
    {

        yield return new WaitForSeconds(m_moveTime);

        RuneData.RuneUseControl = true;
        RuneData.RuneActive = false;
        PlayerData.PlayerIsGetRune = true;
        m_isStatueInteraction = false;
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        m_statue.GetComponent<RuneStatue>().isActive = true;
    }

    public void Initialized()
    {
        transform.position = m_origin;
    }
}
