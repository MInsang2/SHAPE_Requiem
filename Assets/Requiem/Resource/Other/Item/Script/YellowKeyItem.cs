using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowKeyItem : Item
{

    private void Awake()
    {
        m_name = "YellowKey";
        m_ID = 2;
        gameObject.layer = (int)LayerName.Item;
        m_image = DataController.ItemSprites[m_ID];
        m_collider = GetComponent<Collider2D>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
