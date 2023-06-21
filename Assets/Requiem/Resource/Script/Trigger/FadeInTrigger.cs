using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInTrigger : Trigger_Requiem
{
    public Image blackFade; // 검은색 이미지를 Inspector에서 설정해주세요.
    public float fadeSpeed = 0.8f; // 페이드 스피드를 조절합니다.

    // 씬이 시작될 때 페이드 인 효과를 실행합니다.
    void Start()
    {
        StartCoroutine(FadeInEffect());
    }

    // 페이드 인 효과를 처리하는 코루틴입니다.
    IEnumerator FadeInEffect()
    {
        // 검은색으로 화면을 설정합니다.
        blackFade.color = new Color(0f, 0f, 0f, 1f);

        // 투명색으로 페이드 아웃 시작
        while (blackFade.color.a > 0f)
        {
            float newAlpha = blackFade.color.a - (Time.deltaTime * fadeSpeed);
            blackFade.color = new Color(0f, 0f, 0f, newAlpha);
            yield return null;
        }
    }
}