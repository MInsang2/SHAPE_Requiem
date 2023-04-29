// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // 아이템 획득
    // 최초 획득 시 UI 안내

    public int m_ID; // 아이템 일렬번호
    public string m_name; // 아이템 이름
    public Sprite m_image; // 아이템 이미지
    public Animator m_animator; // 아이템 애니매이터
    public string m_explanation; // 아이템 설명
    protected Collider2D m_collider; // 아이템 콜라이더
    

    private void Awake()
    {
        
    }
}
