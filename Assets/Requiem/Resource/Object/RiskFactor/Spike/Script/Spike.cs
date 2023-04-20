using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : RiskFactor
{
    void Start()
    {
        m_name = EnemyData.StaticEnemyNameArr[0];
        m_damage = 1;
    }

    void Update()
    {

    }
}
