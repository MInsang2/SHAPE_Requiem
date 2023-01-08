using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_SystemGPT : MonoBehaviour
{
    [SerializeField] float m_resetDelay;
    [SerializeField] float m_recorverDelay;
    [SerializeField] float m_pushForce;
    [SerializeField] PlayerController m_playerController;
    [SerializeField] Rigidbody2D m_rigid;
    [SerializeField] Collider2D m_colider;
    [SerializeField] GameObject m_hitEffect;
    [SerializeField] Animator m_animator;
    [SerializeField] GameObject m_PlayerMoveSound;
    [SerializeField] CameraFollow m_mainCamera;
    [SerializeField] float m_verticalDistance;
    [SerializeField] float m_horizontalDistance;

    RaycastHit2D[] m_hitInfo = new RaycastHit2D[2];
    float m_timeLeft;
    bool m_isInvincibility = false;
    bool m_loseControl = false;
    bool m_isDead = false;

    void Start()
    {
        m_hitEffect.SetActive(false);
    }

    void Update()
    {
        PlayerStateUpdate();
        ReControl();
        ReControlDead();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        VerticalCaughtCheck();

        if (collision.gameObject.layer == (int)LayerName.RiskFactor && !m_isInvincibility)
        {
            RiskFactorCheck(collision);
        }

        if (collision.gameObject.layer == (int)LayerName.Enemy && !m_isInvincibility)
        {
            EnemyCheck(collision);
        }
    }

    void RiskFactorCheck(Collision2D collision)
    {
        string FactorName = collision.gameObject.name;

        switch (FactorName)
        {
            case "spike":
                HitRiskFactor(collision.gameObject, DataController.SpikeDamage);
                break;
            case "fallingPlatform":
                HitRiskFactor(collision.gameObject, DataController.FallingPlatformDamage);
                break;
            default:
                break;
        }
    }

    void EnemyCheck(Collision2D collision)
    {
        string EnemyName = collision.gameObject.GetComponent<Enemy>().m_name;

        switch (EnemyName)
        {
            case "Bat":
                HitEnemy(collision.gameObject, collision.gameObject.GetComponent<Enemy>().m_damage);
                break;
            default:
                break;
        }
    }

    void PlayerStateUpdate()
    {
        m_playerController.enabled = !m_loseControl;
        m_hitEffect.SetActive(m_loseControl);
    }

    void HitRiskFactor(GameObject riskFactor, int damage)
    {
        DataController.PlayerHP -= damage;
        if (DataController.PlayerHP > 0)
        {
            m_animator.SetTrigger("IsHit");
            m_loseControl = true;
            m_hitEffect.SetActive(true);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = riskFactor.GetComponent<Spike>().m_resetPoint;
            DataController.PlayerIsHit = true;
            m_PlayerMoveSound.SetActive(false);
        }
        else //dead
        {
            Dead();
        }
    }

    void HitEnemy(GameObject _Enemy, int _damage)
    {
        DataController.PlayerHP -= _damage;
        if (DataController.PlayerHP > 0)
        {
            m_animator.SetTrigger("IsHit");
            m_loseControl = true;
            m_hitEffect.SetActive(true);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Vector2 pushDirection = (transform.position - _Enemy.transform.position).normalized;
            m_rigid.AddForce(pushDirection * m_pushForce, ForceMode2D.Impulse);
            DataController.PlayerIsHit = true;
            m_PlayerMoveSound.SetActive(false);
        }
        else //dead
        {
            Dead();
        }
    }

    void ReControl()
    {
        if (m_loseControl)
        {
            if (m_timeLeft < m_resetDelay)
            {
                m_isInvincibility = true;
                m_timeLeft += Time.deltaTime;
            }
            else
            {
                m_loseControl = false;
                m_timeLeft = 0f;
                DataController.PlayerIsHit = false;
                m_isInvincibility = false; 
            }
        }
    }

    void ReControlDead()
    {
        if (m_isDead)
        {
            if (m_timeLeft < m_recorverDelay)
            {
                m_isInvincibility = true;
                m_timeLeft += Time.deltaTime;
            }
            else
            {
                m_isDead = false;
                m_timeLeft = 0f;
                DataController.PlayerIsHit = false;
                DataController.PlayerIsDead = false;
                m_isInvincibility = false;
                m_mainCamera.FollowTime = DataController.CameraFollowTime;
            }
        }
    }

    void Dead()
    {
        DataController.PlayerHP = DataController.PlayerMaxHP;
        m_animator.SetTrigger("IsDead");
        m_loseControl = true;
        m_isDead = true;
        DataController.PlayerIsDead = true;
        m_hitEffect.SetActive(true);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = DataController.PlayerSavePoint;
        m_PlayerMoveSound.SetActive(false);
        m_mainCamera.FollowTime = 0.5f;
        DataController.PlayerDeathCount++;
    }

    void VerticalCaughtCheck()
    {
        m_hitInfo[0] = Physics2D.Raycast(transform.position, Vector2.up, m_verticalDistance, DataController.Platform);
        m_hitInfo[1] = Physics2D.Raycast(transform.position, Vector2.down, m_verticalDistance, DataController.Platform);

        for (int i = 0; i < m_hitInfo.Length; i++)
        {
            if (m_hitInfo[i].collider == null)
                return;
            else
                Dead();
        }
    }

    void HorizontalCaughtCheck()
    {
        m_hitInfo[0] = Physics2D.Raycast(m_colider.bounds.center, Vector2.left, m_horizontalDistance, DataController.Platform);
        m_hitInfo[1] = Physics2D.Raycast(m_colider.bounds.center, Vector2.right, m_horizontalDistance, DataController.Platform);

        for (int i = 0; i < m_hitInfo.Length; i++)
        {
            if (m_hitInfo[i].collider == null)
                return;
            else
                Dead();
        }
    }
}