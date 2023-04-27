using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StaticTrigger : MonoBehaviour
{
    [SerializeField] Enemy_Static m_object;
    [SerializeField] AudioClip triggerAudioClip;
    [SerializeField] AudioSource rockAdioSource;
    [SerializeField] AudioClip rockAudioClip;
    AudioSource m_triggerAdioSource;
    bool m_isActive = false;


    void Start()
    {
        m_triggerAdioSource = GetComponent<AudioSource>();

        if (m_triggerAdioSource == null) Debug.Log("m_triggerAdioSource == null");
        if (m_object == null) Debug.Log("m_object == null");
        if (triggerAudioClip == null) Debug.Log("triggerAudioClip == null");
        if (rockAdioSource == null) Debug.Log("rockAdioSource == null");
        if (rockAudioClip == null) Debug.Log("rockAudioClip == null");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player && !m_isActive)
        {
            m_object.TriggerOn();
            m_triggerAdioSource.PlayOneShot(triggerAudioClip);
            rockAdioSource.PlayOneShot(rockAudioClip);
            m_isActive = true;
        }
    }
}
