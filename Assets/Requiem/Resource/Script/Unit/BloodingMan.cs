using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BloodingMan : MonoBehaviour
{
    // 순차적으로 이동할 포인트들
    public Transform[] pointTransforms;
    Vector3[] points;

    public GameObject hitEffectBig;

    // 이동 속도
    public float speed = 1.0f;

    // 이동 유형 (선형 또는 부드럽게)
    public Ease easeType = Ease.Linear;

    [SerializeField] public AudioSource walkSound;
    [SerializeField] public AudioSource breathSound;
    [SerializeField] AudioSource screamSound;

    public float delayLoadScene;

    private void Awake()
    {
        walkSound.gameObject.SetActive(false);
        breathSound.gameObject.SetActive(false);
        screamSound.gameObject.SetActive(false);
    }

    private void Start()
    {
        hitEffectBig.SetActive(false);

        points = new Vector3[pointTransforms.Length];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = pointTransforms[i].position;
            pointTransforms[i].parent = null;
        }
    }

    // 특정 포인트를 순차적으로 이동
    public void MoveAlongPoints()
    {
        if (points.Length > 0)
        {
            // 시퀀스 생성
            Sequence s = DOTween.Sequence();

            // 각 포인트로의 이동을 시퀀스에 추가
            foreach (Vector3 point in points)
            {
                s.Append(transform.DOMove(point, speed));
            }

            // 시퀀스 시작
            s.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Snake")
        {
            hitEffectBig.SetActive(true);
            transform.parent = collision.transform;
            Destroy(GetComponent<Rigidbody2D>());
            //Destroy(GetComponent<BloodingMan>());
            walkSound.gameObject.SetActive(false);
            breathSound.gameObject.SetActive(false);
            screamSound.gameObject.SetActive(true);

            Invoke("ChangeScene", delayLoadScene);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("underground_2", LoadSceneMode.Single);
    }
}
