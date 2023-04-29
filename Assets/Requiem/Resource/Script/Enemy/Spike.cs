// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Enemy_Static
{
    void Start()
    {
        m_name = EnemyData.StaticEnemyNameArr[0];// 이름 설정
        damage = 1; // 데미지 설정
    }

    void Update()
    {

    }
}
