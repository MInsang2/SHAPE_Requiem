using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueDestroyTrigger : Trigger_Requiem
{
    [SerializeField] Transform snake;
    [SerializeField] Animator snakeAni;
    Vector2 snakeOrigin;


    private void Start()
    {
        snakeOrigin = snake.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<RuneStatue>() != null)
        {
            Destroy(collision.gameObject);
            Invoke("SnakePlayEat", 2f);
            Invoke("SnakeToOrigin", 2f);
        }
    }

    void SnakeToOrigin()
    {
        snake.position = snakeOrigin;
    }

    void SnakePlayEat()
    {
        snakeAni.Play("Eat");
    }
}
