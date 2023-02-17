using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] m_gameObject;
    [SerializeField] AudioClip m_clip;
    [SerializeField] AudioClip m_clip1;
    AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Platform)
        {
            m_audioSource.PlayOneShot(m_clip);
            for (int i = 0; i < m_gameObject.Length; i++)
            {
                Destroy(m_gameObject[i]);
            }
            m_audioSource.PlayOneShot(m_clip1);
        }
    }
}
