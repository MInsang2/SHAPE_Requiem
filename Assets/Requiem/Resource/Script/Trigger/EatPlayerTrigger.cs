using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EatPlayerTrigger : Trigger_Requiem
{
    [SerializeField] Transform snake;
    [SerializeField] Animator snakeAni;
    [SerializeField] float chaseTime;
    Transform player;

    bool playerIn = false;

    private void Start()
    {
        player = PlayerData.PlayerObj.transform;
    }

    private void Update()
    {
        if (playerIn)
        {
            snake.DOMove(player.position + (Vector3.up * 2), chaseTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == player)
        {
            snakeAni.Play("Snake_Bite");
            playerIn = true;
        }
    }
}
