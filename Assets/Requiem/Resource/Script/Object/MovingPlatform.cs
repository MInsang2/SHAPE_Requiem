// 1차 리펙토링

using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Sprite activeSprite; // 활성화 시 이미지
    [SerializeField] private Sprite inactiveSprite; // 비 활성화 시 이미지
    [SerializeField] private AudioClip audioClip; // 효과음

    Switch platformSwitch; // 플랫폼 스위치
    Collider2D platformCollider; // 플랫폼 콜라이더
    SpriteRenderer platformRenderer; // 플랫폼 스프라이트 렌더러
    GameObject light1; // 플랫폼 라이트1
    GameObject light2; // 플랫폼 라이트2
    AudioSource audioSource; // 플랫폼 오디오 소스

    private bool isActivated = false; // 플랫폼 활성화 여부

    public bool IsActivated
    {
        get { return isActivated; }
        set
        {
            if (value != isActivated)
            {
                isActivated = value;
                OnActivationChanged();
            }
        }
    }

    private void Start()
    {
        platformSwitch = transform.Find("Swich").GetComponent<Switch>();
        platformCollider = transform.Find("Platform").GetComponent<Collider2D>();
        platformRenderer = transform.Find("Platform").GetComponent<SpriteRenderer>();
        audioSource = transform.Find("Platform").GetComponent<AudioSource>();
        light1 = platformCollider.transform.Find("Light1").gameObject;
        light2 = platformCollider.transform.Find("Light2").gameObject;

        if (platformSwitch == null) Debug.Log("platformSwitch == null");
        if (platformCollider == null) Debug.Log("platformCollider == null");
        if (platformRenderer == null) Debug.Log("platformRenderer == null");
        if (audioSource == null) Debug.Log("audioSource == null");
        if (light1 == null) Debug.Log("light1 == null");
        if (light2 == null) Debug.Log("light2 == null");
        if (activeSprite == null) Debug.Log("activeSprite == null");
        if (inactiveSprite == null) Debug.Log("inactiveSprite == null");
        if (audioClip == null) Debug.Log("audioClip == null");

        if (light2 != null)
            light2.SetActive(false); // 초기에는 라이트2 비활성화
    }

    private void Update()
    {
        MovePlatform();
    }

    private void OnActivationChanged() // 플랫폼 활성화 상태가 변경될 때마다 효과음 재생
    {
        audioSource.PlayOneShot(audioClip);
    }

    private void MovePlatform()
    {
        if (platformSwitch.isActive)
        {
            platformRenderer.sprite = inactiveSprite; // 플랫폼 비활성화 상태 스프라이트로 변경
            platformCollider.enabled = false; // 플랫폼 비활성화

            if (light1 != null)
                light1.SetActive(false);

            if (light2 != null)
                light2.SetActive(true);
        }
        else
        {
            platformRenderer.sprite = activeSprite; // 플랫폼 활성화 상태 스프라이트로 변경
            platformCollider.enabled = true; 

            if (light1 != null)
                light1.SetActive(true);

            if (light2 != null)
                light2.SetActive(false);
        }
    }
}
