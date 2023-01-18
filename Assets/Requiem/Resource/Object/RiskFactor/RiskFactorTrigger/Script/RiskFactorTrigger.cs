using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiskFactorTrigger : MonoBehaviour
{
    [SerializeField] RiskFactor m_object;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            m_object.TriggerOn();
        }
    }
}
