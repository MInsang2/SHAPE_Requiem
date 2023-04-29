using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BrigeTrigger1 : MonoBehaviour
{
    [SerializeField] Camera m_main;
    [SerializeField] CameraFollow m_mainFollow;
    [SerializeField] float m_size;
    [SerializeField] Vector3 m_pos;
    [SerializeField] float m_changeTime;

    float m_originSize;

    void Start()
    {
        m_originSize = m_main.orthographicSize;
    }

    void Update()
    {
        if (DataController.PlayerIn)
        {
            m_mainFollow.enabled = false;
            DOTween.To(() => m_main.orthographicSize, x => m_main.orthographicSize = x, m_size, m_changeTime);
            DOTween.To(() => m_main.transform.position, x => m_main.transform.position = x, m_pos, m_changeTime);
        }
        else
        {
            m_mainFollow.enabled = true;
            DOTween.To(() => m_main.orthographicSize, x => m_main.orthographicSize = x, m_originSize, 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            //카메라가 확 바뀐다.
            DataController.PlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            //카메라가 확 바뀐다.
            DataController.PlayerIn = false;
        }
    }
}
