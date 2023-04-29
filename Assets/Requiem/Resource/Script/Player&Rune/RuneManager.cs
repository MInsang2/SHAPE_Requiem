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
    /// 룬이 특정 콜라이더 위에  머물 시 발동(트리거),
    /// 룬이 정지하게 된다.
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
        if (collision.gameObject.GetComponent<RuneStatue>() != null && RuneData.RuneActive)
        {
            m_statue = collision.transform;
            m_isStatueInteraction = true;
        }
    }

    /// <summary>
    /// 물에 머물 경우 발동
    /// </summary>
    public void EnterWater()
    {
        RuneData.RuneTouchWater = true;
        RuneData.RuneActive = false;
        RuneData.RuneOnWater = true;
        RuneData.RuneLightArea.enabled = false;
    }

    /// <summary>
    /// 물에서 나갈 경우 발동
    /// </summary>
    public void ExitWater()
    {
        RuneData.RuneOnWater = false;
    }

    /// <summary>
    /// 룬 시야 범위를 변경한다.
    /// </summary>
    void ChangeLightArea()
    {
        RuneData.RuneLightArea.radius = m_luneLight.pointLightOuterRadius;
    }

    public void StatueInteraction(Transform _target)
    {
        m_runeControl.m_target = _target.position;
        RuneData.RuneUseControl = false;
        transform.Rotate(Vector3.back * m_rotationSpeed);
        transform.DOMove(m_runeControl.m_target, m_moveTime);
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
