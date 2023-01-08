using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] LuneManager m_lune;
    [SerializeField] string m_luneName;


    private void Start()
    {
        m_lune = GameObject.Find(m_luneName).GetComponent<LuneManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case (int)LayerName.Lune:
                m_lune.EnterWater();
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case (int)LayerName.Lune:
                m_lune.ExitWater();
                break;
            default:
                break;
        }
    }
}
