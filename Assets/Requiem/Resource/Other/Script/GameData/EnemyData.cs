using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] string[] m_dynamicEnemyNameArr;
    [SerializeField] string[] m_staticEnemyNameArr;

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

    public EnemyData() { }

    public static string[] DynamicEnemyNameArr
    {
        get { return Instance.m_dynamicEnemyNameArr; }
    }
    public static string[] StaticEnemyNameArr
    {
        get { return Instance.m_staticEnemyNameArr; }
    }

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
}
