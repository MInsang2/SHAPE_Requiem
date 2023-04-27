using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// DOMOVE 함수에 사용됨
    /// 카메라가 지정된 위치로 이동 시 걸리는 시간
    /// </summary>
    public float FollowTime;

    /// <summary>
    /// 카메라가 플레이어 보다 얼만큼 앞에 있을 지 정하는 변수
    /// </summary>
    public float CameraPosX;

    /// <summary>
    /// 카메라가 플레이어 보다 얼만큼 위에 있을 지 정하는 변수
    /// </summary>
    public float CameraPosY;

    /// <summary>
    /// 플레이어의 위치
    /// </summary>
    [SerializeField] Transform target;

    /// <summary>
    /// 최종적인 카메라의 위치를 저장하는 변수
    /// </summary>
    Vector3 newPos;

    private void Start()
    {
        DataController.CameraFollowTime = FollowTime;
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        FallowingPlayer();
    }

    /// <summary>
    /// DOMOVE 함수를 이용하여 카메라가 플레이어를 따라오게 만든다.
    /// </summary>
    void FallowingPlayer()
    {
        GetNewPosition();
        transform.DOMove(newPos, FollowTime);
    }


    /// <summary>
    /// 카메라의 새 포지션을 정해주는 함수
    /// 플레이어의 위치를 기준으로 한다.
    /// 플레이어의 회전 값에 따라 x값이 더해지거나 빼진다.
    /// </summary>
    void GetNewPosition()
    {
        if (target.rotation.y == 0)
        {
            newPos = new Vector3(target.position.x + CameraPosX, target.position.y + CameraPosY, -10f);
        }
        else
        {
            newPos = new Vector3(target.position.x - CameraPosX, target.position.y + CameraPosY, -10f);
        }
    }
}
