using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BloodManMoveTrigger : MonoBehaviour
{
    [SerializeField] BloodingMan bloodingMan;
    [SerializeField] GameObject player;
    [SerializeField] CameraFollow mainCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bloodingMan.MoveAlongPoints();
            PlayerData.PlayerIsMove = false;
            player.GetComponent<RuneControllerGPT>().enabled = false;
            player.GetComponent<PlayerControllerGPT>().m_PlayerMoveSound.SetActive(false);
            player.GetComponent<PlayerControllerGPT>().enabled = false;
            PlayerData.PlayerObj.GetComponent<Animator>().Play("PlayerIdle");
            Destroy(PlayerData.PlayerObj.GetComponent<Animator>());
            PlayerData.PlayerObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            PlayerData.PlayerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            mainCamera.target = bloodingMan.transform;
            mainCamera.CameraPosX = 0f;
            mainCamera.CameraPosY = 0f;
            DataController.MainCamera.GetComponent<Camera>().orthographicSize = 7f;
            DOTween.To(() => DataController.MainCamera.GetComponent<Camera>().orthographicSize,
                x => DataController.MainCamera.GetComponent<Camera>().orthographicSize = x, 6f, 5f);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, gameObject.tag);
    }
#endif
}
