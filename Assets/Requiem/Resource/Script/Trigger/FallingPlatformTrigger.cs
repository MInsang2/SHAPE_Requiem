using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FallingPlatformTrigger : MonoBehaviour
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, gameObject.tag);
    }
#endif
}
