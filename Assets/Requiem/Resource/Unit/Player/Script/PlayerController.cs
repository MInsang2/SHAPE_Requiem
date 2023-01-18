
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;


public class PlayerController : MonoBehaviour
{
    public static Action a_Initialized;

    Rigidbody2D m_rigid;
    Animator m_animator;
    Collider2D m_feetCollider;
    Vector2 m_origin;

    [Header("Camera Sysyem")]
    [SerializeField] CameraFollow m_mainCamera;
    [SerializeField] float m_CameraFollowTime;
    [SerializeField] float m_Distance;

    [Header("Sound System")]
    [SerializeField] GameObject m_playerMoveSound;
    [SerializeField] AudioSource m_moveSoundSource;
    [SerializeField] AudioSource m_playerJumpSound;
    [SerializeField] AudioClip[] m_playerSounds;

    [Header("Jump System")]
    [SerializeField] int m_jumpLeft;
    [SerializeField] float m_jumpForce;
    [SerializeField] float m_fallForce;
    [SerializeField] float m_maxFallSpeed;
    [SerializeField] float m_castDistance;

    [SerializeField] bool m_isJump;
    [SerializeField] bool m_isGrounded;
    [SerializeField] bool m_isUpAndDown;

    private void Awake()
    {
        a_Initialized = () => { Initialized(); };

        m_rigid = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_isJump = true;
        m_isUpAndDown = true;
        m_feetCollider = GetComponent<Collider2D>();
        m_playerMoveSound.SetActive(false);
        m_origin = transform.position;
    }

    private void Start()
    {
        m_jumpLeft = DataController.PlayerJumpLeft;
    }

    private void FixedUpdate()
    {
        FastFall();
    }

    private void Update()
    {
        CameraController();
        PlayerDataUpdate();
        Move();
        JumpController();
    }

    void PlayerDataUpdate()
    {
        m_isGrounded = GroundCheck();
        m_isUpAndDown = VerticalMoveCheck();

        AnimationController();

        if (m_isGrounded)
        {
            m_jumpLeft = DataController.PlayerJumpLeft;
            m_isJump = false;
        }
    }

    void Move()
    {
        float dir = Input.GetAxisRaw("Horizontal");

        if (!DataController.PlayerIsHit && !DataController.PlayerIsDead)
        {
            if (dir > 0)
            {
                // move right
                m_rigid.velocity = new Vector2(dir * DataController.PlayerSpeed, m_rigid.velocity.y);
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                m_animator.SetBool("IsMove", true);
                m_animator.SetTrigger("Recorver");
                if (m_rigid.velocity.y == 0)
                {
                    m_playerMoveSound.SetActive(true);
                }
            }
            else if (dir < 0)
            {
                // move left
                m_rigid.velocity = new Vector2(dir * DataController.PlayerSpeed, m_rigid.velocity.y);
                transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
                m_animator.SetBool("IsMove", true);
                m_animator.SetTrigger("Recorver");
                if (m_rigid.velocity.y == 0)
                {
                    m_playerMoveSound.SetActive(true);
                }
            }
            else
            {
                // stop
                m_rigid.velocity = new Vector2(0, m_rigid.velocity.y);
                m_animator.SetBool("IsMove", false);
                m_playerMoveSound.SetActive(false);
            }
        }

    }

    void JumpController()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !DataController.PlayerIsDead)
        {
            Jump();
        }
    }

    void Jump()
    {
        if (!m_isJump && m_jumpLeft > 0)
        {
            m_rigid.velocity = new Vector2(m_rigid.velocity.x, 0f);
            m_rigid.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
            m_animator.SetBool("IsJump", true);
            m_animator.SetBool("IsGround", false);
            m_animator.SetTrigger("IsJumpStart");
            m_playerMoveSound.SetActive(false);
            m_playerJumpSound.PlayOneShot(m_playerSounds[0]);
            m_isUpAndDown = true;

            m_jumpLeft--;

            if (m_jumpLeft <= 0)
            {
                m_isJump = true;
            }
        }
    }

    bool GroundCheck()
    {
        RaycastHit2D hitInfo = Physics2D.BoxCast(m_feetCollider.bounds.center, m_feetCollider.bounds.size, 0f,
                                                            -Vector2.up, m_castDistance, DataController.Platform);

        return hitInfo.collider != null;
    }

    void FastFall()
    {
        if (m_rigid.velocity.y < 0 && m_rigid.velocity.y > -m_maxFallSpeed)
        {
            m_rigid.velocity += Vector2.up * Physics.gravity.y * m_fallForce * Time.deltaTime;
        }
    }

    void CameraController()
    {
        if (Vector2.Distance(m_mainCamera.transform.position, transform.position) > m_Distance)
        {
            m_mainCamera.FollowTime = m_CameraFollowTime;
        }
        else
        {
            m_mainCamera.FollowTime = DataController.CameraFollowTime;
        }
    }

    bool VerticalMoveCheck()
    {
        return m_rigid.velocity.y != 0;
    }

    void AnimationController()
    {
        if (m_isGrounded && m_isUpAndDown)
        {
            m_animator.SetBool("IsGround", true);
        }

        if (m_isGrounded && m_rigid.velocity.y == 0)
        {
            m_animator.SetBool("IsJump", false);
            m_isJump = false;
        }
    }

    public void Initialized()
    {
        transform.position = m_origin;
    }
}
