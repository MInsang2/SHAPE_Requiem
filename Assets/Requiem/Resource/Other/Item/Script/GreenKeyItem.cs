using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenKeyItem : Item
{

    private void Awake()
    {
        m_name = "greenKey";
        m_ID = 1;
        gameObject.layer = (int)LayerName.Item;
    }

    void Update()
    {
        
    }
}
