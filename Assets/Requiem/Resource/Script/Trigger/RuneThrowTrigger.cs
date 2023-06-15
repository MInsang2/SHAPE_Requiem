using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RuneThrowTrigger : MonoBehaviour
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, gameObject.tag);
    }
#endif
}
