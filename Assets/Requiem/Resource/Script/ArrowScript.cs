// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : Enemy_Dynamic
{
    [SerializeField] float destroyTime; // 화살이 사라지는 시간
    [SerializeField] float speed; // 화살의 속도
    Rigidbody2D rb; // 리지드 바디 컴포넌트

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_name = EnemyData.DynamicEnemyNameArr[1];
        damage = 1;
        rb.velocity = transform.right * speed; // 화살의 속도 설정
        Invoke("ArrowDestroy", destroyTime); // 일정 시간 후 화살 파괴
    }

    public override void TriggerOn()
    {

    }

    // 화살 파괴 메소드
    public void ArrowDestroy()
    {
        Destroy(gameObject);
    }

    // 화살의 회전 설정 메소드
    public void SetRotation(Quaternion _quaternion)
    {
        transform.rotation = _quaternion;
    }

    // 트리거 충돌 처리 메소드
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // 충돌한 오브젝트가 플레이어일 경우
        {
            Destroy(gameObject); // 화살 파괴
        }
    }
}
