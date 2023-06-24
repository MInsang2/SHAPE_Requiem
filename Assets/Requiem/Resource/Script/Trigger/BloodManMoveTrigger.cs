using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class BloodManMoveTrigger : Trigger_Requiem
{
    [SerializeField] BloodingMan bloodingMan;
    [SerializeField] GameObject player;
    [SerializeField] CinemachineVirtualCamera mainCM;
    [SerializeField] Camera mainCamera;

    private void Start()
    {
        player = PlayerData.PlayerObj;
        mainCM = DataController.MainCM;
        mainCamera = DataController.MainCamera.GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bloodingMan.walkSound.gameObject.SetActive(true);
            bloodingMan.breathSound.gameObject.SetActive(true);
            bloodingMan.MoveAlongPoints();
            PlayerData.PlayerIsMove = false;
            player.GetComponent<RuneControllerGPT>().enabled = false;
            player.GetComponent<PlayerControllerGPT>().m_PlayerMoveSound.SetActive(false);
            player.GetComponent<PlayerControllerGPT>().enabled = false;
            PlayerData.PlayerObj.GetComponent<Animator>().Play("PlayerIdle");
            Destroy(PlayerData.PlayerObj.GetComponent<Animator>());
            PlayerData.PlayerObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            PlayerData.PlayerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            mainCM.Follow = bloodingMan.transform;
            DOTween.To(() => mainCM.m_Lens.OrthographicSize, x => mainCM.m_Lens.OrthographicSize = x, 6f, 5f);
            DOTween.To(() => mainCamera.orthographicSize, x => mainCamera.orthographicSize = x, 6f, 5f);
        }
    }
}
