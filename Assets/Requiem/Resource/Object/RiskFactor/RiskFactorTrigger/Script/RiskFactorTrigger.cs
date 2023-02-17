using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiskFactorTrigger : MonoBehaviour
{
    [SerializeField] RiskFactor m_object;
    [SerializeField] AudioClip m_triggerAudioClip;
    [SerializeField] AudioSource m_rockAdioSource;
    [SerializeField] AudioClip m_rockAudioClip;
    AudioSource m_triggerAdioSource;
    bool m_isActive = false;


    void Start()
    {
        m_triggerAdioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player && !m_isActive)
        {
            m_object.TriggerOn();
            m_triggerAdioSource.PlayOneShot(m_triggerAudioClip);
            m_rockAdioSource.PlayOneShot(m_rockAudioClip);
            m_isActive = true;
        }
    }
}
