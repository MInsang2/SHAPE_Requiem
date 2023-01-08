using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LuneControllerGPT : MonoBehaviour
{
    /// <summary>
    /// 룬 GameObject
    /// </summary>
    [SerializeField] GameObject m_luneObj;

    /// <summary>
    /// 룬 Rigidbody
    /// </summary>
    [SerializeField] Rigidbody2D m_luneRigid;

    /// <summary>
    /// 룬 시야
    /// </summary>
    [SerializeField] Light2D m_luneLight;

    /// <summary>
    /// 룬의 목표 지점
    /// </summary>
    [SerializeField] public Vector2 m_target;

    /// <summary>
    /// 룬이 플레이어 주변에 있을 때 좌표
    /// </summary>
    [SerializeField] Vector2 m_lunePosition;

    /// <summary>
    /// 룬이 목표 지점으로 가는 시간
    /// </summary>
    [SerializeField] float m_moveTime;

    /// <summary>
    /// 룬이 움직일 때 나는 소리
    /// </summary>
    [SerializeField] AudioSource m_luneSound;

    /// <summary>
    /// 룬이 발사 상태인지 체크하는 변수
    /// </summary>
    [SerializeField] bool m_isShoot;

    /// <summary>
    /// 룬 발사 딜레이 타임
    /// </summary>
    [SerializeField] float m_shootDelayTime;

    [SerializeField] bool m_isMouseDelay = false;

    void Start()
    {
        DataController.LuneActive = false;
        m_luneObj.transform.parent = null;
        m_luneObj.SetActive(true);
        m_target = transform.position;
        m_isShoot = false;
        m_luneSound.volume = 0.2f;
    }

    void Update()
    {
        LuneControl();
        LuneSoundController();
        LuneMove();
    }

    /// <summary>
    /// 룬의 전반적인 컨트롤을 담당하는 함수
    /// </summary>
    void LuneControl()
    {
        if (DataController.LuneUseControl)
        {
            if (Input.GetMouseButtonDown(0) && !m_isMouseDelay)
            {
                if (m_isShoot)
                {
                    m_isShoot = false;
                    m_isMouseDelay = true;
                    StartCoroutine("MouseClickDelay");
                }
                else
                {
                    m_isShoot = true;

                    if (!DataController.LuneTouchWater)
                    {
                        DataController.LuneActive = true;
                        DataController.LuneLightArea.enabled = true;
                    }
                    ChangeTargetToMouse();

                    m_isMouseDelay = true;
                    StartCoroutine("MouseClickDelay");
                }
            }
            else if (!m_isShoot)
            {
                ReturnLune();
            }

            if (DataController.LuneTouchWater)
            {
                LunePowerLose();
            }

            LunePowerBack();
        }

    }


    /// <summary>
    /// 룬을 도착 지점으로 이동 시키는 함수
    /// 계속 반복 실행 중
    /// </summary>
    void LuneMove()
    {
        m_luneObj.transform.DOMove(m_target, m_moveTime);
    }

    /// <summary>
    /// 룬의 도착지점을 마우스 포인터로 옮겨주는 함수
    /// </summary>
    void ChangeTargetToMouse()
    {
        m_target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    /// <summary>
    /// 룬 사운드를 조절해주는 함수
    /// </summary>
    void LuneSoundController()
    {
        if (m_isShoot)
        {
            // 파라미터 설명(조절 할 변수, 조절 할 변수, 목표 값, 소요 시간)
            DOTween.To(() => m_luneSound.volume, x => m_luneSound.volume = x, 1f, 1f);
        }
        else
        {
            DOTween.To(() => m_luneSound.volume, x => m_luneSound.volume = x, 0f, 0.4f);
        }
    }


    /// <summary>
    /// 룬의 포지션을 플레이어 주위로 변경시켜주는 함수
    /// </summary>
    void ReturnLune()
    {
        if (transform.rotation.y == 0)
        {
            // 오른쪽 진행 시
            m_target = new Vector2(transform.position.x + m_lunePosition.x, transform.position.y + m_lunePosition.y);
        }
        else if (transform.rotation.y != 0f)
        {
            // 왼쪽 진행 시
            m_target = new Vector2(transform.position.x + (-m_lunePosition.x), transform.position.y + m_lunePosition.y);
        }
        DataController.LuneActive = m_isShoot;
        DataController.LuneLightArea.enabled = false;
    }

    /// <summary>
    /// 룬의 시야를 회복한다.
    /// </summary>
    void LunePowerBack()
    {
        if (Vector2.Distance(transform.position, m_luneObj.transform.position) <= DataController.LunePowerBackDistance && !DataController.LuneOnWater)
        {
            DataController.LuneTouchWater = false;
            DataController.LuneLightArea.enabled = true;
            DOTween.To(() => m_luneLight.pointLightOuterRadius, x => m_luneLight.pointLightOuterRadius = x, DataController.LuneOuterRadius, DataController.LunePowerBackTime);
        }
    }

    void LunePowerLose()
    {
        DOTween.To(() => m_luneLight.pointLightOuterRadius, x => m_luneLight.pointLightOuterRadius = x, 0f, DataController.LunePowerBackTime);
    }


    /// <summary>
    /// 룬이 제자리에 멈춘다.
    /// </summary>
    public void LuneStop()
    {
        m_target = m_luneObj.transform.position;
    }

    IEnumerator MouseClickDelay()
    {
        // wait for the specified delay
        yield return new WaitForSeconds(m_shootDelayTime);

        // reset the mouseClicked flag
        m_isMouseDelay = false;
    }
}