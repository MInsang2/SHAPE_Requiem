using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SnakeTrigger : MonoBehaviour
{
    [SerializeField] SnakeOpning snake;

    BoxCollider2D m_collider2D;

    private void Start()
    {
        m_collider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            snake.SnakeActive();
        }
    }

    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, gameObject.tag);
    }
}
