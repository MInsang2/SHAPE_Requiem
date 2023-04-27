using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class GetLuneTrigger : MonoBehaviour
{
    [SerializeField] RuneStatue runeStatue;
    [SerializeField] RuneManager runeManager;
    [SerializeField] Light2D m_light;
    [SerializeField] float animationTime;

    public float rotationSpeed = 10f; // The speed at which the object rotates
    public float convergenceSpeed = 1f; // The speed at which the object converges towards the target

    bool m_isActive = false;

    private void Start()
    {
        if (runeStatue == null) Debug.Log("runeStatue == null");
        if (runeManager == null) Debug.Log("runeManager == null");
        if (m_light == null) Debug.Log("m_light == null");
    }

    void Update()
    {
        if (m_isActive)
        {
            runeManager.transform.Rotate(Vector3.forward * rotationSpeed);

            runeManager.transform.position = 
                Vector2.MoveTowards(runeManager.transform.position, runeStatue.transform.position, Time.deltaTime * convergenceSpeed);

            DOTween.To(() => m_light.intensity, x => m_light.intensity = x, 0f, animationTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_isActive && collision.gameObject.layer == (int)LayerName.Player && !PlayerData.PlayerIsGetRune)
        {
            m_isActive = true;
            // 룬 애니매이션 시작
            runeStatue.EnterTheLune();
            runeStatue.m_isActive = true;
            StartCoroutine("GetLuneDelay");
        }
    }

    IEnumerator GetLuneDelay()
    {
        yield return new WaitForSeconds(animationTime);

        m_isActive = false;
        runeManager.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        PlayerData.PlayerIsGetRune = true;
    }
}
