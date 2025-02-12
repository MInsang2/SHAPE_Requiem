// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityEngine.SceneManagement; //이걸 써야지 씬에 관한 시스템을 적용시킬수있다.

public class PlayerControllerGPT : MonoBehaviour
{
    [Header("점프 시스템")]
    [SerializeField] int m_jumpLeft; // 남은 점프 횟수
    [SerializeField] float m_jumpForce; // 점프 파워
    [SerializeField] float m_fallForce; // 낙하 속도
    [SerializeField] float m_maxFallSpeed; // 최대 낙하 속도
    [SerializeField] float m_castDistance; // 땅과의 충돌 판정
    [SerializeField] LayerMask m_platform; // 플랫폼 레이어 마스크
    [SerializeField] ParticleSystem m_randingEffect;

    bool m_isJump; // 점프 상태 체크
    bool m_isGrounded; // 땅 접촉 상태 체크

    // 플레이어의 컴포넌트
    [SerializeField] public GameObject m_PlayerMoveSound;
    [SerializeField] Rigidbody2D m_rigid;
    [SerializeField] Animator m_animator;
    [SerializeField] Collider2D m_collider;



    private void Start()
    {
        // 컴포넌트를 가져와 변수에 할당
        m_PlayerMoveSound = PlayerData.PlayerMoveSoundSource.gameObject;
        m_rigid = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider2D>();
        m_randingEffect = transform.Find("RandingEffect").GetComponent<ParticleSystem>();

        if (m_PlayerMoveSound == null) Debug.Log("m_PlayerMoveSound == null");
        if (m_rigid == null) Debug.Log("m_rigid == null");
        if (m_animator == null) Debug.Log("m_animator == null");
        if (m_collider == null) Debug.Log("m_collider == null");
        if (m_randingEffect == null) Debug.Log("m_randingEffect == null");

        // 변수 초기값 설정
        m_jumpLeft = PlayerData.PlayerJumpLeft;
        m_PlayerMoveSound.SetActive(false);
        m_isJump = true;
    }

    private void FixedUpdate()
    {
        FastFall(); // 빠른 추락 처리
    }

    private void Update()
    {
        if (PlayerData.PlayerIsMove)
        {
            PlayerDataUpdate(); // 플레이어 데이터 업데이트
            Move(); // 이동 처리
            JumpController(); // 점프 제어
        }
    }

    private void PlayerDataUpdate()
    {
        bool isGrounded = GroundCheck(); // 바닥 확인

        if (isGrounded && !m_isGrounded) // 지상에 있고, 이전에 지상이 아니었을 경우
        {
            m_jumpLeft = PlayerData.PlayerJumpLeft;
            m_isJump = false;

            m_randingEffect.Play();
        }

        m_isGrounded = isGrounded;

        AnimationController(); // 애니메이션 제어
    }

    private void Move()
    {
        float dir = Input.GetAxisRaw("Horizontal"); // 수평 방향 입력 받기

        if (!PlayerData.PlayerIsHit && !PlayerData.PlayerIsDead) // 플레이어가 피격되지 않았고, 사망하지 않았다면
        {
            if (dir > 0)
            {
                // 오른쪽으로 이동
                m_rigid.velocity = new Vector2(dir * PlayerData.PlayerSpeed, m_rigid.velocity.y);
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                m_animator.SetBool("IsMove", true);
                m_animator.SetTrigger("Recorver");

                // 수직 속도가 거의 0일 때 이동 소리 활성화
                if (Mathf.Abs(m_rigid.velocity.y) < 0.1)
                {
                    m_PlayerMoveSound.SetActive(true);
                }
                else
                {
                    m_PlayerMoveSound.SetActive(false);
                }
            }
            else if (dir < 0)
            {
                // 왼쪽으로 이동
                m_rigid.velocity = new Vector2(dir * PlayerData.PlayerSpeed, m_rigid.velocity.y);
                transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
                m_animator.SetBool("IsMove", true);
                m_animator.SetTrigger("Recorver");

                // 수직 속도가 거의 0일 때 이동 소리 활성화
                if (Mathf.Abs(m_rigid.velocity.y) < 0.1)
                {
                    m_PlayerMoveSound.SetActive(true);
                }
                else
                {
                    m_PlayerMoveSound.SetActive(false);
                }
            }
            else
            {
                // 멈춤
                m_rigid.velocity = new Vector2(0, m_rigid.velocity.y);
                m_animator.SetBool("IsMove", false);
                m_PlayerMoveSound.gameObject.SetActive(false);
            }
        }
    }

    private void JumpController()
    {
        // 스페이스바를 누르고, 플레이어가 죽지 않았으며, 점프 중이 아닐 때
        if (Input.GetKeyDown(KeyCode.Space) && !PlayerData.PlayerIsDead && !m_isJump)
        {
            m_animator.SetTrigger("IsJump");  // 점프 애니메이션 트리거
            Jump(); // 점프 실행
        }
    }

    private void Jump()
    {
        if (m_jumpLeft <= 0) return; // 남은 점프 횟수가 없으면 종료

        m_rigid.velocity = new Vector2(m_rigid.velocity.x, 0f); // 수직 속도 초기화
        m_rigid.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse); // 점프 힘 적용

        // 점프 소리 재생
        if (m_jumpLeft > 1)
            PlayerData.PlayerJumpSoundSource.PlayOneShot(PlayerData.PlayerJumpAudioClip[0]);
        else if (m_jumpLeft == 1)
            PlayerData.PlayerJumpSoundSource.PlayOneShot(PlayerData.PlayerJumpAudioClip[1]);

        m_jumpLeft--; // 남은 점프 횟수 감소

        if (m_jumpLeft <= 0) m_isJump = true; // 남은 점프 횟수가 없으면 점프 중으로 설정
    }

    private bool GroundCheck()
    {
        // 상자 캐스트로 땅과의 충돌 검사
        RaycastHit2D hitInfo = Physics2D.BoxCast(m_collider.bounds.center, m_collider.bounds.size, 0f, -Vector2.up, m_castDistance, m_platform);

        if (hitInfo.collider != null)
        {
            return true; // 땅과 충돌했으면 true 반환
        }
        else
        {
            // 레이캐스트로 땅과의 충돌 검사
            RaycastHit2D hitInfo2 = Physics2D.Raycast(m_collider.bounds.center, -Vector2.up, m_castDistance, m_platform);

            if (hitInfo2.collider != null)
            {
                return true; // 땅과 충돌했으면 true 반환
            }
        }

        return false; // 땅과 충돌하지 않았으면 false 반환
    }

    private void FastFall()
    {
        // 플레이어가 아래로 떨어지고 있으며, 최대 낙하 속도에 도달하지 않았을 때
        if (m_rigid.velocity.y < 0 && m_rigid.velocity.y > -m_maxFallSpeed)
        {
            // 더 빠른 낙하 힘 적용
            m_rigid.velocity += Vector2.up * Physics.gravity.y * m_fallForce * Time.deltaTime;
        }
    }

    private bool VerticalMoveCheck()
    {
        return m_rigid.velocity.y != 0; // 수직 이동 여부 반환
    }

    private void AnimationController()
    {
        m_animator.SetBool("IsGround", m_isGrounded); // 지상 여부 애니메이션 파라미터 설정
    }
}