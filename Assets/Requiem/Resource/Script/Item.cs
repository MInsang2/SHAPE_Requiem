using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // 아이템 획득
    // 최초 획득 시 UI 안내

    public int m_ID; // 아이템 일렬번호
    public string m_name; // 아이템 이름
    public Sprite m_image;
    public Animator m_animator;
    public string m_explanation;
    protected Collider2D m_collider;
    

    private void Awake()
    {
        
    }
}
