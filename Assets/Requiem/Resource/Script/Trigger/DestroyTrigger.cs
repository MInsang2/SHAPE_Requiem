// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : Trigger_Requiem
{
    [SerializeField] GameObject[] gameObjectArr; // 파괴되는 오브젝트들
    [SerializeField] AudioClip[] clipArr; // 파괴 될 때 사운드들
    AudioSource audioSource; // 자신의 오디오 소스

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null) Debug.Log("m_audioSource == null");
        if (gameObjectArr.Length == 0) Debug.Log("gameObjectArr.Length == 0");
        if (clipArr.Length == 0) Debug.Log("clipArr.Length == 0");

        for (int i = 0; i < gameObjectArr.Length; i++)
            if (gameObjectArr[i] == null) Debug.Log($"gameObjectArr[{i}] == null");

        for (int i = 0; i < clipArr.Length; i++)
            if (clipArr[i] == null) Debug.Log($"clipArr[{i}] == null");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 트리거 작동 시 호출
        if (collision.gameObject.layer == (int)LayerName.Platform)
        {
            audioSource.PlayOneShot(clipArr[0]);
            for (int i = 0; i < gameObjectArr.Length; i++)
            {
                Destroy(gameObjectArr[i]);
            }
            audioSource.PlayOneShot(clipArr[1]);
        }
    }
}
