using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestoryThis());
    }

    private void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(DestroyThisOnTouch());
    }

    IEnumerator DestoryThis()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    IEnumerator DestroyThisOnTouch()
    {
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
}
