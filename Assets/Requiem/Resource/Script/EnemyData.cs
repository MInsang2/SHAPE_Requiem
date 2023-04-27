// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [Header("동적 에너미 정보")]
    [SerializeField] string[]       dynamicEnemyNameArr; // 동적 적 이름 배열
    [SerializeField] AudioClip[]    dynamicEnemyAudioClipArr; // 동적 적 오디오 배열

    [Header("정적 에너미 정보")]
    [SerializeField] string[]       staticEnemyNameArr; // 정적 적 이름 배열
    [SerializeField] AudioClip[]    staticEnemyAudioClipArr; // 정적 적 오디오 배열

    [Header("투사체 정보")]
    [SerializeField] GameObject[]   projectileArr; // 투사체 오브젝트 배열

    // 싱글톤 인스턴스
    private static EnemyData instance = null;

    // 인스턴스에 접근할 수 있는 프로퍼티
    public static EnemyData Instance
    {
        get
        {
            // 인스턴스가 없으면 생성
            if (instance == null)
            {
                instance = new EnemyData();
            }
            return instance;
        }
    }


    
    public static string[] DynamicEnemyNameArr // 동적 적 이름 배열에 접근할 수 있는 프로퍼티
    {
        get { return Instance.dynamicEnemyNameArr; }
    }
    public static string[] StaticEnemyNameArr // 정적 적 이름 배열에 접근할 수 있는 프로퍼티
    {
        get { return Instance.staticEnemyNameArr; }
    }
    public static AudioClip[] DynamicEnemyAudioClipArr // 동적 적 오디오 배열에 접근할 수 있는 프로퍼티
    {
        get { return Instance.dynamicEnemyAudioClipArr; }
    }
    public static AudioClip[] StaticEnemyAudioClipArr // 정적 적 오디오 배열에 접근할 수 있는 프로퍼티
    {
        get { return Instance.staticEnemyAudioClipArr; }
    }
    public static GameObject[] ProjectileArr // 투사체 오브젝트 배열에 접근할 수 있는 프로퍼티
    {
        get { return Instance.projectileArr; }
    }

    // 컴포넌트가 깨어날 때 인스턴스 할당
    private void Awake()
    {
        if (GameObject.Find("DataController") == null)
        {
            if (instance == null)
            {
                instance = GetComponent<EnemyData>();
            }
        }
    }

    private void Start()
    {
        if (DynamicEnemyNameArr.Length == 0) Debug.Log("DynamicEnemyNameArr.Length == 0");
        if (StaticEnemyNameArr.Length == 0) Debug.Log("StaticEnemyNameArr.Length == 0");
        if (DynamicEnemyAudioClipArr.Length == 0) Debug.Log("DynamicEnemyAudioClipArr.Length == 0");
        if (StaticEnemyAudioClipArr.Length == 0) Debug.Log("StaticEnemyAudioClipArr.Length == 0");
        if (ProjectileArr.Length == 0) Debug.Log("ProjectileArr.Length == 0");

        for (int i = 0; i < DynamicEnemyNameArr.Length; i++)
        {
            if (DynamicEnemyNameArr[i] == null) Debug.Log($"DynamicEnemyNameArr[{i}] == null");
        }

        for (int i = 0; i < StaticEnemyNameArr.Length; i++)
        {
            if (StaticEnemyNameArr[i] == null) Debug.Log($"StaticEnemyNameArr[{i}] == null");
        }

        for (int i = 0; i < DynamicEnemyAudioClipArr.Length; i++)
        {
            if (DynamicEnemyAudioClipArr[i] == null) Debug.Log($"DynamicEnemyAudioClipArr[{i}] == null");
        }

        for (int i = 0; i < StaticEnemyAudioClipArr.Length; i++)
        {
            if (StaticEnemyAudioClipArr[i] == null) Debug.Log($"StaticEnemyAudioClipArr[{i}] == null");
        }

        for (int i = 0; i < ProjectileArr.Length; i++)
        {
            if (ProjectileArr[i] == null) Debug.Log($"ProjectileArr[{i}] == null");
        }
    }
}
