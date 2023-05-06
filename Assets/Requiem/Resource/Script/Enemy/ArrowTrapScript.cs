// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrapScript : MonoBehaviour
{
    [SerializeField] Transform firepoint; // 발사 위치
    [SerializeField] GameObject arrow; // 화살 오브젝트
    [SerializeField] float destroyTime; // 화살이 사라지는 시간
    [SerializeField] float speed; // 화살의 속도
    [SerializeField] float shootingDelay = 0; // 발사 속도

    private void Start()
    {
        arrow = EnemyData.ProjectileArr[0];
        firepoint = transform.Find("FirePoint");

        if (arrow == null) Debug.Log("Arrow == null");
        if (firepoint == null) Debug.Log("firepoint == null");

        StartCoroutine(Shoot());
    }

    // 코루틴을 사용한 발사 함수
    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootingDelay); // 발사 속도만큼 대기
            ArrowScript newArrow = 
                Instantiate(arrow, firepoint.position, firepoint.rotation).GetComponent<ArrowScript>();

            newArrow.SetArrow(destroyTime, speed, transform.rotation);
        }
    }
}
