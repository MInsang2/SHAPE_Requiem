using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class GetLuneTrigger : MonoBehaviour
{
    [SerializeField] RuneStatue m_runeStatue;
    [SerializeField] RuneManager m_luneManager;
    [SerializeField] Light2D m_light;
    [SerializeField] float m_animationTime;

    public float rotationSpeed = 10f; // The speed at which the object rotates
    public float convergenceSpeed = 1f; // The speed at which the object converges towards the target

    bool m_isActive = false;

    void Update()
    {
        if (m_isActive)
        {
            // Rotate the object around its z-axis
            m_luneManager.transform.Rotate(Vector3.forward * rotationSpeed);

            // Move the object towards the target
            m_luneManager.transform.position = Vector2.MoveTowards(m_luneManager.transform.position, m_runeStatue.transform.position, Time.deltaTime * convergenceSpeed);

            DOTween.To(() => m_light.intensity, x => m_light.intensity = x, 0f, m_animationTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_isActive && collision.gameObject.layer == (int)LayerName.Player && !PlayerData.PlayerIsGetRune)
        {
            m_isActive = true;
            // 룬 애니매이션 시작
            m_runeStatue.EnterTheLune();
            m_runeStatue.m_isActive = true;
            StartCoroutine("GetLuneDelay");
        }
    }

    IEnumerator GetLuneDelay()
    {
        yield return new WaitForSeconds(m_animationTime);

        m_isActive = false;
        m_luneManager.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        PlayerData.PlayerIsGetRune = true;
    }
}
