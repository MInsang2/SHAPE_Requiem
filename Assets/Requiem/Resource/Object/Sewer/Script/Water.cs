using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] RuneManager m_rune;


    private void Start()
    {
        m_rune = RuneData.RuneObj.GetComponent<RuneManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case (int)LayerName.Rune:
                m_rune.EnterWater();
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case (int)LayerName.Rune:
                m_rune.ExitWater();
                break;
            default:
                break;
        }
    }
}
