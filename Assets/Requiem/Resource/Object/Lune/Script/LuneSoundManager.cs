using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuneSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip m_clip1;
    [SerializeField] AudioClip m_clip2;
    AudioSource m_audioSource;


    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void PlayLuneOn()
    {
        m_audioSource.PlayOneShot(m_clip1);
    }

    public void PlayLuneOff()
    {
        m_audioSource.PlayOneShot(m_clip2);
    }
}
