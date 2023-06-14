using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeOpning : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;
    [SerializeField] float snakeMoveSpeed;

    private void Start()
    {
        rigid.bodyType = RigidbodyType2D.Static;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "BrokenBrige")
        {
            rigid.gravityScale = snakeMoveSpeed;
            animator.SetBool("EatActive", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "BrokenBrige")
        {
            rigid.gravityScale = snakeMoveSpeed;
            animator.SetBool("EatActive", true);
        }
    }

    public void SnakeActive()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        rigid.gravityScale = -snakeMoveSpeed;
    }
}
