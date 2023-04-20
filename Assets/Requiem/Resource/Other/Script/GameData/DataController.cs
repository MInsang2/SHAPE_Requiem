using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


//레이어 번호
public enum LayerName
{
    Default,
    TransparentFX,
    IgnoreRaycast,
    Player,
    Water,
    UI,
    Rune,
    Wall,
    Platform,
    RiskFactor,
    Enemy,
    LightArea,
    MossLight,
    Teleport,
    Item
}

[Serializable]
class CameraData
{
    public GameObject m_mainCamera;
    public float m_followTime;
}

[Serializable]
public class SoundManager
{
    public float m_bgmVolume;
    public float m_runeSoundVolume;
    public float m_walkSoundVolume;
    public float m_jumpSoundVolume;
}

[Serializable]
public class TriggerData
{
    public bool m_playerIn; 
}

[Serializable]
public class ItemData
{
    public GameObject m_canvasObj;
    public Sprite[] m_sprite = new Sprite[100];
    public bool m_isInvenOpen = false;
}



public class DataController : MonoBehaviour
{
    static DataController instance = null;

    [SerializeField] CameraData m_cameraData = new CameraData();
    [SerializeField] SoundManager m_soundManager = new SoundManager();
    [SerializeField] TriggerData m_triggerData = new TriggerData();
    [SerializeField] ItemData m_itemData = new ItemData();



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

    // 메인 카메라 데이터
    public static GameObject MainCamera
    {
        get { return instance.m_cameraData.m_mainCamera; }
        set { instance.m_cameraData.m_mainCamera = value; }
    }
    public static float CameraFollowTime
    {
        get { return instance.m_cameraData.m_followTime; }
        set { instance.m_cameraData.m_followTime = value; }
    }

    // 사운드매니저
    public static float BGMVolume
    {
        get { return instance.m_soundManager.m_bgmVolume; }
        set { instance.m_soundManager.m_bgmVolume = value; }
    }
    public static float LuneSoundVolume
    {
        get { return instance.m_soundManager.m_runeSoundVolume; }
        set { instance.m_soundManager.m_runeSoundVolume = value; }
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

    // 아이템 데이터
    public static GameObject CanvasObj
    {
        get { return instance.m_itemData.m_canvasObj; }
        set { instance.m_itemData.m_canvasObj = value; }
    }
    public static Sprite[] ItemSprites
    {
        get { return instance.m_itemData.m_sprite; }
    }
    public static bool IsInvenOpen
    {
        get { return instance.m_itemData.m_isInvenOpen; }
        set { instance.m_itemData.m_isInvenOpen = value; }
    }

    private void Awake()
    {
        if (GameObject.Find("DataController") == null)
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        if (m_cameraData.m_mainCamera == null)
        {
            m_cameraData.m_mainCamera = GameObject.Find("Main Camera");
        }

        if (m_itemData.m_canvasObj == null)
        {
            m_itemData.m_canvasObj = GameObject.Find("Canvas");
        }

        DataController.PlayerIn = false;
    }
}
