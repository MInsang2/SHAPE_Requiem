using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameStart : MonoBehaviour
{
    // 처음에 얘가 누워있어야 한다. -> 방향키를 누르면 일어남.
    // 이후에 룬을 획득하기 전까지 룬이 없어야 한다.

    [SerializeField] Transform m_player;
    [SerializeField] Transform m_lune;
    

    Animator m_playerAni;

    void Start()
    {
        DataController.PlayerIsMove = false;
        DataController.PlayerIsGetLune = false;
        m_playerAni = m_player.GetComponent<Animator>();
        m_playerAni.SetBool("IsFirstStart", true);
    }

    void Update()
    {
        GetMoveKeyCheck();
    }

    void GetMoveKeyCheck()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            m_playerAni.SetBool("IsFirstStart", false);
            StartCoroutine(GetKeyAD());
        }
    }

    IEnumerator GetKeyAD()
    {
        yield return new WaitForSeconds(1f);

        DataController.PlayerIsMove = true;
    }
}
