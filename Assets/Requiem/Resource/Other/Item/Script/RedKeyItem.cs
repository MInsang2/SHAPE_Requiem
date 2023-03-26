using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKeyItem : Item
{

    private void Awake()
    {
        m_name = "redKey";
        m_ID = 0;
        gameObject.layer = (int)LayerName.Item;
    }

    void Update()
    {
        
    }
}
