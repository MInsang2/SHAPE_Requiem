using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[Serializable]
public class PlayerData // 플레이어 데이터
{
    public GameObject m_playerObj; // 플레이어 오브젝트
    [Header("Player Movement")]
    public Rigidbody2D m_playerRigid; // 플레이어 리지드바디
    public Animator m_playerAnimator; // 플레이어 애니매이터
    public float m_playerSpeed; // 플레이어 이동속도
    public int m_jumpLeft; // 플레이어 점프 횟수
    [Header("Player HP_System")]
    public int m_maxHP; // 최대 체력
    public int m_HP; // 현재 체력
    public bool m_isDead; // 죽음 체크
    public bool m_isHit; // 맞음 판정
    public bool m_isMove;
    public bool m_isGetLune;
    public Vector2 m_savePoint;
    public uint m_deathCount;
}

[Serializable]
class CameraData
{
    public float m_followTime;
}

[Serializable]
public class LuneData // 룬 데이터
{
    public GameObject m_luneObj; // 룬 오브젝트
    public Rigidbody2D m_luneRigid; // 룬 리지드바디
    public Light2D m_luneSight; // 룬 시야
    public CircleCollider2D m_luneLightArea; // 룬 시야 충돌 범위
    public float m_luneIntensity; // 룬 시야 밝기
    public float m_luneOuterRadius; // 룬 시야 범위
    public float m_lunePowerBackDistance; // 룬 힘 회복 거리
    public float m_lunePowerBackTime; // 룬 힘 회복 시간
    public float m_minVolume; // 룬 최소 볼륨
    public bool m_isStop; // 룬 정지 상태인가
    public bool m_isReturn; // 룬 리턴 상태인가
    public bool m_isActive; // 룬 활성화 상태인가
    public bool m_onWater; // 룬이 물에 들어가 있는가
    public bool m_touchWater; // 룬이 물에 닿았는가
    public bool m_useControl; // 룬을 움직일 수 있는가
}

[Serializable]
public class SoundManager
{
    public float m_bgmVolume;
    public float m_luneSoundVolume;
    public float m_walkSoundVolume;
    public float m_jumpSoundVolume;
}

[Serializable]
public class TriggerData
{
    public bool m_playerIn; 
}

//레이어 번호
public enum LayerName
{
    Default,
    TransparentFX,
    IgnoreRaycast,
    Player,
    Water,
    UI,
    Lune,
    Wall,
    Platform,
    RiskFactor,
    Enemy,
    LightArea,
    MossLight
}

public class DataController : MonoBehaviour
{


    static DataController instance = null;



    [SerializeField] PlayerData m_playerData = new PlayerData();
    [SerializeField] CameraData m_cameraData = new CameraData();
    [SerializeField] LuneData m_luneData = new LuneData();
    [SerializeField] SoundManager m_soundManager = new SoundManager();
    [SerializeField] TriggerData m_triggerData = new TriggerData();



    public static DataController Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }

            return instance;
        }
    }



    //플레이어
    public static GameObject PlayerObj
    {
        get { return instance.m_playerData.m_playerObj; }
    }
    public static Rigidbody2D PlayerRigid
    {
        get { return instance.m_playerData.m_playerRigid; }
    }
    public static Animator PlayerAnimator
    {
        get { return instance.m_playerData.m_playerAnimator; }
    }
    public static float PlayerSpeed
    {
        get { return instance.m_playerData.m_playerSpeed; }
        set { instance.m_playerData.m_playerSpeed = value; }
    }
    public static int PlayerJumpLeft
    {
        get { return instance.m_playerData.m_jumpLeft; }
        set { instance.m_playerData.m_jumpLeft = value; }
    }
    public static int PlayerMaxHP
    {
        get { return instance.m_playerData.m_maxHP; }
        set { instance.m_playerData.m_maxHP = value; }
    }
    public static int PlayerHP
    {
        get { return instance.m_playerData.m_HP; }
        set { instance.m_playerData.m_HP = value; }
    }
    public static Vector2 PlayerSavePoint
    {
        get { return instance.m_playerData.m_savePoint; }
        set { instance.m_playerData.m_savePoint = value; }
    }
    public static bool PlayerIsDead
    {
        get { return instance.m_playerData.m_isDead; }
        set { instance.m_playerData.m_isDead = value; }
    }
    public static bool PlayerIsHit
    {
        get { return instance.m_playerData.m_isHit; }
        set { instance.m_playerData.m_isHit = value; }
    }
    public static bool PlayerIsMove
    {
        get { return instance.m_playerData.m_isMove; }
        set { instance.m_playerData.m_isMove = value; }
    }
    public static bool PlayerIsGetLune
    {
        get { return instance.m_playerData.m_isGetLune; }
        set { instance.m_playerData.m_isGetLune = value; }
    }
    public static uint PlayerDeathCount
    {
        get { return instance.m_playerData.m_deathCount; }
        set { instance.m_playerData.m_deathCount = value; }
    }



    // 메인 카메라 데이터
    public static float CameraFollowTime
    {
        get { return instance.m_cameraData.m_followTime; }
        set { instance.m_cameraData.m_followTime = value; }
    }



    //던지는 룬
    public static GameObject LuneObj
    {
        get { return instance.m_luneData.m_luneObj; }
    }
    public static Rigidbody2D LuneRigid
    {
        get { return instance.m_luneData.m_luneRigid; }
    }
    public static Light2D LuneSight
    {
        get { return instance.m_luneData.m_luneSight; }
        set { instance.m_luneData.m_luneSight = value; }
    }
    public static CircleCollider2D LuneLightArea
    {
        get { return instance.m_luneData.m_luneLightArea; }
        set { instance.m_luneData.m_luneLightArea = value; }
    }
    public static bool LuneIsStop
    {
        get { return instance.m_luneData.m_isStop; }
        set { instance.m_luneData.m_isStop = value; }
    }
    public static bool LuneIsReturn
    {
        get { return instance.m_luneData.m_isReturn; }
        set { instance.m_luneData.m_isReturn = value; }
    }
    public static bool LuneActive
    {
        get { return instance.m_luneData.m_isActive; }
        set { instance.m_luneData.m_isActive = value; }
    }

    public static float LuneIntensity
    {
        get { return instance.m_luneData.m_luneIntensity; }
        set { instance.m_luneData.m_luneIntensity = value; }
    }
    public static float LunePowerBackDistance
    {
        get { return instance.m_luneData.m_lunePowerBackDistance; }
        set { instance.m_luneData.m_luneIntensity = value; }
    }
    public static float LunePowerBackTime
    {
        get { return instance.m_luneData.m_lunePowerBackTime; }
        set { instance.m_luneData.m_lunePowerBackTime = value; }
    }
    public static bool LuneOnWater
    {
        get { return instance.m_luneData.m_onWater; }
        set { instance.m_luneData.m_onWater = value; }
    }
    public static float LuneOuterRadius
    {
        get { return instance.m_luneData.m_luneOuterRadius; }
        set { instance.m_luneData.m_luneOuterRadius = value; }
    }
    public static bool LuneTouchWater
    {
        get { return instance.m_luneData.m_touchWater; }
        set { instance.m_luneData.m_touchWater = value; }
    }
    public static bool LuneUseControl
    {
        get { return instance.m_luneData.m_useControl; }
        set { instance.m_luneData.m_useControl = value; }
    }
    public static float LuneMinVolume
    {
        get { return instance.m_luneData.m_minVolume; }
        set { instance.m_luneData.m_minVolume = value; }
    }


    // 사운드매니저
    public static float BGMVolume
    {
        get { return instance.m_soundManager.m_bgmVolume; }
        set { instance.m_soundManager.m_bgmVolume = value; }
    }
    public static float LuneSoundVolume
    {
        get { return instance.m_soundManager.m_luneSoundVolume; }
        set { instance.m_soundManager.m_luneSoundVolume = value; }
    }
    public static float WalkSoundVolume
    {
        get { return instance.m_soundManager.m_walkSoundVolume; }
        set { instance.m_soundManager.m_walkSoundVolume = value; }
    }
    public static float JumpSoundVolume
    {
        get { return instance.m_soundManager.m_jumpSoundVolume; }
        set { instance.m_soundManager.m_jumpSoundVolume = value; }
    }

    // 트리거 데이터
    public static bool PlayerIn
    {
        get { return instance.m_triggerData.m_playerIn; }
        set { instance.m_triggerData.m_playerIn = value; }
    }



    private void Awake()
    {
        if (GameObject.Find("DataController") == null)
        {
            if (instance == null)
            {
                instance = this;

                //DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                //DontDestroyOnLoad(this.gameObject);
            }
        }
        

        DataController.LuneOuterRadius = DataController.LuneSight.pointLightOuterRadius;
        DataController.LuneOnWater = false;
        DataController.PlayerIsDead = false;
        DataController.PlayerIsHit = false;
        DataController.LuneTouchWater = false;
        DataController.LuneUseControl = true;
        DataController.PlayerIsMove = true;
        DataController.PlayerIsGetLune = true;
        DataController.PlayerIn = false;
    }
}
