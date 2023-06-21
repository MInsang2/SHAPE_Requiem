using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformTrigger : Trigger_Requiem
{
    [SerializeField] GameObject m_fallingPlatform;
    [SerializeField] AudioClip m_clip;
    [SerializeField] AudioSource m_audioSource;

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == m_fallingPlatform)
        {
            m_audioSource.PlayOneShot(m_clip);
        }
    }
}
