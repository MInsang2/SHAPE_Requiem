// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : Enemy_Dynamic
{
    private void Start()
    {
        m_name = EnemyData.DynamicEnemyNameArr[1];
        damage = 1;
    }

    // 화살 설정 메소드
    public void SetArrow(float _destroyTime, float _speed, Quaternion _quaternion)
    {
        transform.rotation = _quaternion;

        GetComponent<Rigidbody2D>().velocity = transform.right * _speed; // 화살의 속도 설정
        Invoke("ArrowDestroy", _destroyTime); // 일정 시간 후 화살 파괴
    }

    // 화살 파괴 메소드
    public void ArrowDestroy()
    {
        Destroy(gameObject);
    }
}
