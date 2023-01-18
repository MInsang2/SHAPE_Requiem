using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : RiskFactor
{
    [SerializeField] LaunchedArrow m_arrowPrefab;
    [SerializeField] float m_shootDelay;
    Vector2 m_launchPoint;

    float m_timeLeft = 0f;
    bool m_isShoot = false;

    private void Start()
    {
        m_launchPoint = transform.GetChild(0).position;
    }

    private void Update()
    {
        Launched();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.LightArea)
        {
            m_isShoot = true;
        }
    }

    void Launched()
    {
        if (m_isShoot)
        {
            if (m_timeLeft < m_shootDelay)
            {
                m_timeLeft += Time.deltaTime;
            }
            else
            {
                float dis = (m_launchPoint - (Vector2)transform.position).magnitude;
                Vector2 dir = (m_launchPoint - (Vector2)transform.position) / dis;

                LaunchedArrow arrow = Instantiate(m_arrowPrefab);
                arrow.transform.position = m_launchPoint;
                arrow.SetArrowDir(dir);
                arrow.SetRotation(transform.rotation);

                m_isShoot = false;
                m_timeLeft = 0f;
            }
        }
    }
}
