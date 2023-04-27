using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Enemy_Static
{
    void Start()
    {
        m_name = EnemyData.StaticEnemyNameArr[0];
        damage = 1;
    }

    void Update()
    {

    }
}
