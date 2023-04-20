using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RuneData : MonoBehaviour
{
    [SerializeField] GameObject m_runeObj; // 룬 오브젝트
    [SerializeField] CircleCollider2D m_runeLightArea; // 룬 시야 충돌 범위
    [SerializeField] float m_runeIntensity; // 룬 시야 밝기
    [SerializeField] float m_runeOuterRadius; // 룬 시야 범위
    [SerializeField] float m_runePowerBackDistance; // 룬 힘 회복 거리
    [SerializeField] float m_runePowerBackTime; // 룬 힘 회복 시간
    [SerializeField] float m_minVolume; // 룬 최소 볼륨
    [SerializeField] bool m_isStop; // 룬 정지 상태인가
    [SerializeField] bool m_isReturn; // 룬 리턴 상태인가
    [SerializeField] bool m_isActive; // 룬 활성화 상태인가
    [SerializeField] bool m_onWater; // 룬이 물에 들어가 있는가
    [SerializeField] bool m_touchWater; // 룬이 물에 닿았는가
    [SerializeField] bool m_useControl; // 룬을 움직일 수 있는가

    private static RuneData instance = null;

    // 인스턴스에 접근할 수 있는 프로퍼티
    public static RuneData Instance
    {
        get
        {
            // 인스턴스가 없으면 생성
            if (instance == null)
            {
                instance = new RuneData();
            }
            return instance;
        }
    }

    public static GameObject RuneObj
    {
        get { return Instance.m_runeObj; }
        set { Instance.m_runeObj = value; }
    }
    public static CircleCollider2D RuneLightArea
    {
        get { return Instance.m_runeLightArea; }
        set { Instance.m_runeLightArea = value; }
    }
    public static bool RuneIsStop
    {
        get { return Instance.m_isStop; }
        set { Instance.m_isStop = value; }
    }
    public static bool RuneIsReturn
    {
        get { return Instance.m_isReturn; }
        set { Instance.m_isReturn = value; }
    }
    public static bool RuneActive
    {
        get { return Instance.m_isActive; }
        set { Instance.m_isActive = value; }
    }
    public static float RuneIntensity
    {
        get { return Instance.m_runeIntensity; }
        set { Instance.m_runeIntensity = value; }
    }
    public static float RunePowerBackDistance
    {
        get { return Instance.m_runePowerBackDistance; }
        set { Instance.m_runeIntensity = value; }
    }
    public static float RunePowerBackTime
    {
        get { return Instance.m_runePowerBackTime; }
        set { Instance.m_runePowerBackTime = value; }
    }
    public static bool RuneOnWater
    {
        get { return Instance.m_onWater; }
        set { Instance.m_onWater = value; }
    }
    public static float RuneOuterRadius
    {
        get { return Instance.m_runeOuterRadius; }
        set { Instance.m_runeOuterRadius = value; }
    }
    public static bool RuneTouchWater
    {
        get { return Instance.m_touchWater; }
        set { Instance.m_touchWater = value; }
    }
    public static bool RuneUseControl
    {
        get { return Instance.m_useControl; }
        set { Instance.m_useControl = value; }
    }
    public static float RuneMinVolume
    {
        get { return Instance.m_minVolume; }
        set { Instance.m_minVolume = value; }
    }




    private void Awake()
    {
        if (GameObject.Find("DataController") == null)
        {
            if (instance == null)
            {
                instance = GetComponent<RuneData>();
            }
        }

        if (m_runeObj == null)
        {
            m_runeObj = GameObject.Find("Rune");
        }

        if (m_runeLightArea == null)
        {
            m_runeLightArea = RuneData.RuneObj.transform.Find("LightArea").GetComponent<CircleCollider2D>();
        }
        
        RuneData.RuneOuterRadius = RuneData.RuneObj.GetComponent<Light2D>().pointLightOuterRadius;
        RuneData.RuneOnWater = false;
        RuneData.RuneTouchWater = false;
        RuneData.RuneUseControl = true;
    }
}
