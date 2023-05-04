// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class RuneControllerGPT : MonoBehaviour
{
    public Vector2 target; // 룬의 이동 목표 위치
    public float moveTime; // 룬 이동 시간
    public bool isShoot; // 룬이 발사되었는지 여부

    [SerializeField] private Vector2 runePosition; // 룬의 초기 위치
    [SerializeField] private Vector2 origin; // 룬의 원점 위치
    [SerializeField] private float shootDelayTime; // 발사 지연 시간
    [SerializeField] private float runeReturnDistance; // 룬이 자동으로 되돌아올 거리
    [SerializeField] private bool isMouseDelay = false; // 마우스 클릭 지연 여부

    private GameObject runeObj; // 룬 게임 오브젝트
    private RuneSoundManager runeSoundManager; // 룬 사운드 관리자
    private Light2D runeSight; // 룬의 조명 컴포넌트
    private LayerMask layerMask; // 충돌 감지 레이어 마스크

    void Start()
    {
        InitializeRuneController(); // 룬 컨트롤러 초기화
    }

    void Update()
    {
        if (PlayerData.PlayerIsGetRune)
        {
            RuneColliding(); // 룬 충돌 처리
            RuneControl(); // 룬 제어
            RuneMove(); // 룬 이동
        }
    }

    private void InitializeRuneController()
    {
        runeObj = RuneData.RuneObj;
        runeSoundManager = RuneData.RuneObj.GetComponent<RuneSoundManager>();
        runeSight = RuneData.RuneObj.GetComponent<Light2D>();
        runeObj.transform.parent = null;
        RuneData.RuneActive = false;
        runeObj.SetActive(true);
        target = transform.position;
        isShoot = false;
        layerMask = LayerMask.GetMask("Platform", "Wall", "RiskFactor");

        ValidateComponents(); // 컴포넌트 유효성 검사
    }

    // 컴포넌트 유효성 검사
    private void ValidateComponents()
    {
        if (runeObj == null) Debug.Log("m_runeObj == null");
        if (runeSoundManager == null) Debug.Log("m_runeSoundManager == null");
        if (runeSight == null) Debug.Log("m_runeSight == null");
    }

    // 룬 제어
    private void RuneControl()
    {
        if (RuneData.RuneUseControl)
        {
            HandleRuneShoot(); // 룬 발사 처리
            HandleRuneReturn(); // 룬 반환 처리
            HandleRunePower(); // 룬 파워 처리
        }
    }

    // 룬 이동
    private void RuneMove()
    {
        runeObj.transform.DOMove(target, moveTime);
    }

    // 마우스 위치로 목표 변경
    private void ChangeTargetToMouse()
    {
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    // 룬 반환
    private void ReturnRune()
    {
        if (transform.rotation.y == 0)
        {
            target = new Vector2(transform.position.x + runePosition.x, transform.position.y + runePosition.y);
        }
        else if (transform.rotation.y != 0f)
        {
            target = new Vector2(transform.position.x + (-runePosition.x), transform.position.y + runePosition.y);
        }
        RuneData.RuneActive = isShoot;
        RuneData.RuneLightArea.enabled = false;
    }

    // 룬 발사 처리
    private void HandleRuneShoot()
    {
        if (Input.GetMouseButtonDown(0) && !isMouseDelay)
        {
            if (isShoot)
            {
                runeSoundManager.PlayRuneOff();
                isShoot = false;
                isMouseDelay = true;
                StartCoroutine("MouseClickDelay");
            }
            else
            {
                isShoot = true;

                if (!RuneData.RuneTouchWater)
                {
                    RuneData.RuneActive = true;
                    RuneData.RuneLightArea.enabled = true;
                }
                ChangeTargetToMouse();
                isMouseDelay = true;
                runeSoundManager.PlayRuneOn();
                StartCoroutine("MouseClickDelay");
            }
        }
        else if (!isShoot)
        {
            ReturnRune();
        }
    }

    // 룬 반환 처리
    private void HandleRuneReturn()
    {
        if (Vector2.Distance(runeObj.transform.position, transform.position) >= RuneReturnDistance())
        {
            isShoot = false;
            isMouseDelay = true;
            runeSoundManager.PlayRuneOff();
            StartCoroutine(MouseClickDelay());
        }
    }

    // 룬 반환 거리 설정
    private float RuneReturnDistance()
    {
        return runeReturnDistance;
    }

    // 룬 파워 처리
    private void HandleRunePower()
    {
        if (RuneData.RuneOnWater)
        {
            RunePowerLose(); // 룬 파워 감소
        }
        else
        {
            RunePowerBack(); // 룬 파워 회복
        }
        RuneReturnDistance();
    }

    // 룬 파워 회복
    private void RunePowerBack()
    {
        if (Vector2.Distance(transform.position, runeObj.transform.position) <= RuneData.RunePowerBackDistance && !RuneData.RuneOnWater)
        {
            RuneData.RuneTouchWater = false;
            RuneData.RuneLightArea.enabled = true;
            DOTween.To(() => runeSight.pointLightOuterRadius, x => runeSight.pointLightOuterRadius = x, RuneData.RuneOuterRadius, RuneData.RunePowerBackTime);
        }
    }

    // 룬 파워 감소
    private void RunePowerLose()
    {
        DOTween.To(() => runeSight.pointLightOuterRadius, x => runeSight.pointLightOuterRadius = x, 0f, RuneData.RunePowerBackTime);
    }

    // 룬 이동 중지
    public void RuneStop()
    {
        target = runeObj.transform.position;
    }

    // 마우스 클릭 지연 코루틴
    private IEnumerator MouseClickDelay()
    {
        yield return new WaitForSeconds(shootDelayTime);
        isMouseDelay = false;
    }

    // 룬 충돌 처리
    private void RuneColliding()
    {
        RaycastHit2D hit =
            Physics2D.Raycast(runeObj.transform.position,
            (target - (Vector2)runeObj.transform.position).normalized,
            Vector2.Distance(runeObj.transform.position, target),
            layerMask);

        if (hit.collider != null &&
            (hit.collider.gameObject.layer == (int)LayerName.Platform ||
            hit.collider.gameObject.layer == (int)LayerName.Wall ||
            hit.collider.gameObject.layer == (int)LayerName.RiskFactor))
        {
            target = hit.point;
        }
    }
}
