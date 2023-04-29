// 1차 리펙토링

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanWindowManager : MonoBehaviour
{
    private GameObject[] explanWindows = new GameObject[32];

    // 시작할 때 설명창 초기화
    private void Start()
    {
        InitializeExplanWindows();
    }

    // 설명창 배열에 각 자식 게임오브젝트를 할당
    private void InitializeExplanWindows()
    {
        for (int i = 0; i < explanWindows.Length; i++)
        {
            explanWindows[i] = transform.GetChild(i).gameObject;
        }
    }

    // 인벤토리가 열리지 않은 경우 설명창 비활성화
    void Update()
    {
        if (!DataController.IsInvenOpen)
        {
            DeactivateAllExplanWindows();
        }
    }

    // 모든 설명창을 비활성화하는 메서드
    private void DeactivateAllExplanWindows()
    {
        for (int i = 0; i < explanWindows.Length; i++)
        {
            explanWindows[i].SetActive(false);
        }
    }
}
