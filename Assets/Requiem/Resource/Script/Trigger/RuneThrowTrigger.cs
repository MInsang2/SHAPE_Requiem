using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneThrowTrigger : Trigger_Requiem
{
    [SerializeField] GameObject runeThrowGuide;

    bool m_isActive = false;

    void Start()
    {
        runeThrowGuide = DataController.CanvasObj.transform.Find("LuneThrowGuide").gameObject;
        runeThrowGuide.SetActive(false);

        if (runeThrowGuide == null) Debug.Log("runeThrowGuide == null");
    }

    void Update()
    {
        if (PlayerData.PlayerIsGetRune)
        {
            runeThrowGuide.SetActive(true);
            m_isActive = true;
        }

        if (m_isActive && Input.GetMouseButtonDown(0))
        {
            runeThrowGuide.SetActive(false);
            Destroy(runeThrowGuide);
            Destroy(this);
        }
    }
}
