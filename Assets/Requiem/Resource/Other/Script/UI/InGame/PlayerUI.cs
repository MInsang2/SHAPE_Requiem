using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    Transform m_PlayerHp;
    [SerializeField] TextMeshProUGUI m_HP;

    void Start()
    {
        m_PlayerHp = transform.Find("HP").Find("Text");
    }

    void Update()
    {
        m_HP.text = DataController.PlayerHP.ToString();
    }
}
