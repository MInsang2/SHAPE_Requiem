using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LuminusPlant3 : MonoBehaviour
{
    [SerializeField] private float m_maxRadius;
    [SerializeField] private float m_minRadius;
    [SerializeField] private float m_lightTime;

    private Light2D m_light;

    private void Awake()
    {
        m_light = GetComponent<Light2D>();
    }

    private void Start()
    {
        StartCoroutine(LuminusBlink());
    }

    private IEnumerator LuminusBlink()
    {
        while (true)
        {
            // Expand light
            yield return DOTween.To(() => m_light.pointLightInnerRadius, x => m_light.pointLightInnerRadius = x, m_maxRadius, m_lightTime).WaitForCompletion();

            // Shrink light
            yield return DOTween.To(() => m_light.pointLightInnerRadius, x => m_light.pointLightInnerRadius = x, m_minRadius, m_lightTime).WaitForCompletion();
        }
    }
}
