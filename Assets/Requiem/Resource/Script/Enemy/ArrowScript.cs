// 1�� �����丵

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowScript : Enemy_Dynamic
{
    public bool isActive = false;
    [SerializeField] float mass = 1f;
    [SerializeField] float shootForce = 10f;
    [SerializeField] float disappearTime = 2f;
    Rigidbody2D rigid;
    Collider2D colli;
    

    private void Start()
    {
        damage = 0;
        rigid = GetComponent<Rigidbody2D>();
        colli = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (isActive == true)
        {
            isActive = false;
            rigid.bodyType = RigidbodyType2D.Dynamic;
            rigid.gravityScale = 0f;
            rigid.angularDrag = 0f;
            rigid.mass = mass;

            ApplyForceBasedOnRotation();

            Invoke("ObjDestroy", 10f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.parent = collision.transform;
            rigid.bodyType = RigidbodyType2D.Kinematic;

            ArrowDestroy();
        }
    }

    private void ApplyForceBasedOnRotation()
    {
        // ���� ��ü�� ȸ������ ������� ���� ���͸� ���
        float rotationInRadians = -transform.rotation.eulerAngles.z * Mathf.Deg2Rad; // ����Ƽ�� �ð���� ȸ���� �����̹Ƿ� ��ȣ�� ����
        Vector2 direction = new Vector2(Mathf.Sin(rotationInRadians), Mathf.Cos(rotationInRadians));

        // ���� �������� ���� ���Ѵ�
        rigid.AddForce(direction.normalized * shootForce, ForceMode2D.Impulse);
    }

    // ȭ�� �ı� �޼ҵ�
    public void ArrowDestroy()
    {
        Color endColor = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0f);
        GetComponent<SpriteRenderer>().DOColor(endColor, disappearTime);
        Invoke("ObjDestroy", disappearTime);
    }

    void ObjDestroy()
    {
        Destroy(gameObject);
    }
}
