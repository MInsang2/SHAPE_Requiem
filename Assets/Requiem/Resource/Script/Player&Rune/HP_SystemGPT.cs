// 1�� �����丵

// ���� ������ �÷��̾��� ü�°� ���� ���θ� �����ϴ� ��ũ��Ʈ
// ����, �÷��̾ ���� �ε����� ���� ó���� ���
// �� ��ũ��Ʈ�� �÷��̾� ������Ʈ�� �پ� �ִ�
// �ʿ��� �������� SerializeField�� Inspector â���� ������ �� �ִ�

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HP_SystemGPT : MonoBehaviour
{
    // �ʱ�ȭ�� ���� �ν����Ϳ��� ������ �� �ֵ��� SerializeField�� ���
    [SerializeField] float resetDelay; // �÷��̾ �ǰݵ� �� ��� ��ã�� ������ �ɸ��� �ð�
    [SerializeField] float recorverDelay; // �÷��̾ ����� �� ��Ȱ�ϴ� �ð�
    [SerializeField] float pushForce; // �÷��̾ �ǰݵ� �� �и��� ��
    [SerializeField] float verticalDistance; // ���� �浹 üũ �Ÿ�
    [SerializeField] float horizontalDistance; // ���� �浹 üũ �Ÿ�
    [SerializeField] LayerMask platform; // �浹�� üũ�� ���̾� ����ũ

    // �÷��̾�� ī�޶�, �ִϸ�����, ������ٵ� ���� ������Ʈ
    PlayerControllerGPT playerController;
    Rigidbody2D rb;
    Collider2D m_collider;
    GameObject hitEffect;
    Animator animator;
    GameObject playerMoveSound;

    // �浹�� üũ�� Raycast ������ ������ �迭 �� ���ʹ� ����
    RaycastHit2D[] hitInfo = new RaycastHit2D[2];
    string[] dynamicEnemyName;
    string[] staticEnemyName;

    // ��� ��ã�� ���� �ð�, ���� ���� ����, ���� �Ұ� ���� ����, ��� ����
    float timeLeft;
    bool isInvincibility = false;
    bool loseControl = false;
    bool isDead = false;
    bool cameraChange = false;

    void Start()
    {
        InitializeVariables(); // ������Ʈ �ʱ�ȭ
    }

    void InitializeVariables()
    {
        // ������Ʈ�� ������ ������ �Ҵ�
        playerController = PlayerData.PlayerObj.GetComponent<PlayerControllerGPT>();
        rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        hitEffect = PlayerData.PlayerObj.transform.Find("HitEffect").gameObject;
        animator = GetComponent<Animator>();
        playerMoveSound = PlayerData.PlayerMoveSoundSource.gameObject;

        if (playerController == null) Debug.Log("playerController == null");
        if (rb == null) Debug.Log("rb == null");
        if (m_collider == null) Debug.Log("m_collider == null");
        if (hitEffect == null) Debug.Log("hitEffect == null");
        if (animator == null) Debug.Log("animator == null");
        if (playerMoveSound == null) Debug.Log("playerMoveSound == null");

        hitEffect.SetActive(false); // �浹 ȿ�� ������Ʈ ��Ȱ��ȭ

        // ���ʹ� ������ �Է� �ޱ�
        dynamicEnemyName = new string[EnemyData.DynamicEnemyNameArr.Length];
        staticEnemyName = new string[EnemyData.StaticEnemyNameArr.Length];

        // �迭�� �����Ͽ� ������ ����
        for (int i = 0; i < dynamicEnemyName.Length; i++)
        {
            dynamicEnemyName[i] = EnemyData.DynamicEnemyNameArr[i];
        }

        for (int i = 0; i < staticEnemyName.Length; i++)
        {
            staticEnemyName[i] = EnemyData.StaticEnemyNameArr[i];
        }
    }

    void Update()
    {
        PlayerStateUpdate(); // �÷��̾� ���� ������Ʈ
        ReControlHit(); // ���� ��ã��
        ReControlDead(); // ��� �� ��Ȱ�ϱ�
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!isInvincibility)
        {
            CheckCollision(collision.gameObject); // �浹 ó��
        }

        if (cameraChange)
        {
            if (collision.gameObject.GetComponent<DivArea>() != null)
            {
                collision.gameObject.GetComponent<DivArea>().ChangeCameraArea();
                cameraChange = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInvincibility)
        {
            CheckCollision(collision.gameObject); // �浹 ó��
        }
    }

    void CheckCollision(GameObject obj) // ���� ������Ʈ�� �浹�� �Ͼ �� ȣ��Ǵ� �޼ҵ�.
    {

        VerticalCaughtCheck(); // �÷��̾ �����ִ��� Ȯ��.


        if (obj.GetComponent<Enemy_Static>() != null) // �浹�� ������Ʈ�� ���� ������� Ȯ��.
        {
            Static_EnemyCheck(obj.GetComponent<Enemy_Static>());
        }


        if (obj.GetComponent<Enemy_Dynamic>() != null) // �浹�� ������Ʈ�� ������ Ȯ��.
        {
            Dynamic_EnemyCheck(obj.GetComponent<Enemy_Dynamic>());
        }
    }


    void Static_EnemyCheck(Enemy_Static _enemy) // ���� ��ҿ��� �浹�� ó���ϴ� �޼ҵ�.
    {
        HitEnemy_Static(_enemy);
    }


    void Dynamic_EnemyCheck(Enemy_Dynamic _enemy) // ������ �浹�� ó���ϴ� �޼ҵ�.
    {
        HitEnemy_Dynamic(_enemy);
    }


    void PlayerStateUpdate() // �÷��̾��� ���¸� ������Ʈ�ϴ� �޼ҵ�
    {

        playerController.enabled = !loseControl; // ���� ��� ���ο� ���� �÷��̾� ��Ʈ�ѷ��� Ȱ��ȭ/��Ȱ��ȭ
        hitEffect.SetActive(loseControl); // ���� ��� ������ �� ��Ʈ ����Ʈ�� Ȱ��ȭ
    }

    void HitEnemy_Static(Enemy_Static _riskFactor) // ���� ��ҿ� �浹 �� ó���ϴ� �޼ҵ�
    {
        PlayerData.PlayerHP -= _riskFactor.GetDamage; // �÷��̾� ü���� ���� ����� ��������ŭ ����

        if (PlayerData.PlayerHP > 0) // ü���� �������� ���
        {
            animator.SetTrigger("IsHit"); // �ִϸ��̼��� �ǰ� ���·� ��ȯ
            loseControl = true; // ���� ��� ���·� ��ȯ
            hitEffect.SetActive(true); // ��Ʈ ����Ʈ�� Ȱ��ȭ
            GetComponent<Rigidbody2D>().velocity = Vector2.zero; // ������ٵ��� �ӵ��� 0���� �����
            transform.position = _riskFactor.resetPoint; // �÷��̾��� ��ġ�� ���� ����� ���� �������� �̵�
            PlayerData.PlayerIsHit = true; // �÷��̾ �ǰݵǾ���
            playerMoveSound.SetActive(false); // �÷��̾� �̵� ���带 ��Ȱ��ȭ
        }
        else // ü���� 0�̸� ����
        {
            Dead();
        }
    }

    void HitEnemy_Dynamic(Enemy_Dynamic _Enemy) // ���� �浹 �� ó���ϴ� �޼ҵ�
    {
        PlayerData.PlayerHP -= _Enemy.GetDamage; // �÷��̾� ü���� ���� ��������ŭ ����

        if (PlayerData.PlayerHP > 0) // ü���� �������� ���
        {
            animator.SetTrigger("IsHit"); // �ִϸ��̼��� �ǰ� ���·� ��ȯ
            loseControl = true; // ���� ��� ���·� ��ȯ
            hitEffect.SetActive(true); // ��Ʈ ����Ʈ�� Ȱ��ȭ
            GetComponent<Rigidbody2D>().velocity = Vector2.zero; // ������ٵ��� �ӵ��� 0���� �����
            Vector2 pushDirection = (transform.position - _Enemy.transform.position).normalized; // �÷��̾ �о ������ ���
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse); // �÷��̾ �о��
            PlayerData.PlayerIsHit = true; // �÷��̾ �ǰݵǾ���
            playerMoveSound.SetActive(false); // �÷��̾� �̵� ���带 ��Ȱ��ȭ

            if (_Enemy.GetComponent<ArrowScript>() != null) // ���� �߻�� ȭ���� ���
            {
                _Enemy.GetComponent<ArrowScript>().ArrowDestroy(); // ȭ���� �ı�
            }
        }
        else // ü���� 0�� ��� ����
        {
            Dead();
        }
    }


    void ReControlHit() // �ǰ� �� �÷��̾� ���� ȸ��
    {
        if (loseControl) // ���� ��� ������ ���
        {
            if (timeLeft < resetDelay) // �ð��� ���� �����ð����� ���� ���
            {
                isInvincibility = true; // ���� ���·� ��ȯ
                timeLeft += Time.deltaTime; // �ð��� ����
            }
            else
            {
                loseControl = false; // ���� ��� ���¸� ����
                timeLeft = 0f; // �ð��� �ʱ�ȭ
                PlayerData.PlayerIsHit = false; // �÷��̾ �ǰݵ��� �ʾ����� ��Ÿ����
                isInvincibility = false; // ���� ���¸� ����
            }
        }
    }


    void ReControlDead() // ���� �� �÷��̾� ���� ȸ��
    {
        if (isDead) // ���� ������ ���
        {
            if (timeLeft < recorverDelay) // �ð��� ���� �����ð����� ���� ���
            {
                isInvincibility = true; // ���� ���·� ��ȯ
                timeLeft += Time.deltaTime; // �ð��� ����
            }
            else
            {
                isDead = false; // ���� ���¸� ����
                timeLeft = 0f;  // �ð��� �ʱ�ȭ
                PlayerData.PlayerIsHit = false; // �÷��̾ �ǰݵ��� �ʾ����� ��Ÿ����
                PlayerData.PlayerIsDead = false; // �÷��̾ ���� �ʾ����� ��Ÿ����
                isInvincibility = false;  // ���� ���¸� ����
            }
        }
    }

    void Dead() // ���� ó���ϴ� �޼ҵ�
    {
        cameraChange = true;
        PlayerData.PlayerHP = PlayerData.PlayerMaxHP; // �÷��̾� ü���� �ִ�ġ�� ����
        animator.SetTrigger("IsDead");  // �ִϸ��̼��� ���� ���·� ��ȯ
        loseControl = true;  // ���� ��� ���·� ��ȯ
        isDead = true; // ���� ���·� ��ȯ
        PlayerData.PlayerIsDead = true; // �÷��̾ �׾����� ��Ÿ����
        hitEffect.SetActive(true); // ��Ʈ ����Ʈ�� Ȱ��ȭ
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; // ������ٵ��� �ӵ��� 0���� �����
        transform.position = PlayerData.PlayerSavePoint; // �÷��̾��� ��ġ�� ���̺� �������� �̵�
        playerMoveSound.SetActive(false); // �÷��̾� �̵� ���带 ��Ȱ��ȭ
        PlayerData.PlayerDeathCount++; // �÷��̾� ��� Ƚ���� ����
    }

    void VerticalCaughtCheck() // �������� �����ִ��� Ȯ���ϴ� �޼ҵ�
    {
        hitInfo[0] = Physics2D.Raycast(transform.position, Vector2.up, verticalDistance, platform);
        hitInfo[1] = Physics2D.Raycast(transform.position, Vector2.down, verticalDistance, platform);

        for (int i = 0; i < hitInfo.Length; i++)
        {
            if (hitInfo[i].collider == null) // �浹ü�� ������ ����
                return;
            else
                Dead(); // ���̸� ���� ó��
        }
    }

    void HorizontalCaughtCheck() // �������� �����ִ��� Ȯ���ϴ� �޼ҵ�
    {
        hitInfo[0] = Physics2D.Raycast(m_collider.bounds.center, Vector2.left, horizontalDistance, platform);
        hitInfo[1] = Physics2D.Raycast(m_collider.bounds.center, Vector2.right, horizontalDistance, platform);

        for (int i = 0; i < hitInfo.Length; i++)
        {
            if (hitInfo[i].collider == null) // �浹ü�� ������ ����
                return;
            else
                Dead(); // ���̸� ���� ó��
        }
    }
}