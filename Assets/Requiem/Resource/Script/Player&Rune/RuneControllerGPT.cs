// 1�� �����丵

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class RuneControllerGPT : MonoBehaviour
{
    public Vector2 target; // ���� �̵� ��ǥ ��ġ
    public float moveTime; // �� �̵� �ð�
    public bool isShoot; // ���� �߻�Ǿ����� ����
    public bool isCharge = false;

    [SerializeField] private Vector2 runePosition; // ���� �ʱ� ��ġ
    [SerializeField] private Vector2 origin; // ���� ���� ��ġ
    [SerializeField] private float shootDelayTime; // �߻� ���� �ð�
    [SerializeField] private float runeReturnDistance; // ���� �ڵ����� �ǵ��ƿ� �Ÿ�
    [SerializeField] private bool isMouseDelay = false; // ���콺 Ŭ�� ���� ����
    [SerializeField] public float batteryDrainSpeed = 1f;
    [SerializeField] public float additionalMovementReduction = 50f;
    [SerializeField] public ParticleSystem runeCharge;
    [SerializeField] SpriteRenderer[] batteryUI;
    [SerializeField] SpriteRenderer batteryBorder;


    private GameObject runeObj; // �� ���� ������Ʈ
    private RuneSoundManager runeSoundManager; // �� ���� ������
    private Light2D runeSight; // ���� ���� ������Ʈ
    private LayerMask layerMask; // �浹 ���� ���̾� ����ũ

    void Start()
    {
        InitializeRuneController(); // �� ��Ʈ�ѷ� �ʱ�ȭ
    }

    void Update()
    {
        if (PlayerData.PlayerIsGetRune)
        {
            RuneColliding(); // �� �浹 ó��
            RuneControl(); // �� ����
            RuneMove(); // �� �̵�
            RuneCharging(); // �� ���� ȿ��
        }
    }

    private void InitializeRuneController()
    {
        runeObj = RuneData.RuneObj;
        runeSoundManager = RuneData.RuneObj.GetComponent<RuneSoundManager>();
        runeSight = RuneData.RuneObj.GetComponent<Light2D>();
        runeObj.transform.parent = null;
        RuneData.RuneActive = false;
        runeObj.SetActive(true);
        target = transform.position;
        isShoot = false;
        layerMask = LayerMask.GetMask("Platform", "Wall", "RiskFactor");

        SetBatteryUIVisible(false, RuneData.RuneBattery / 1000f);

        ValidateComponents(); // ������Ʈ ��ȿ�� �˻�
    }

    // ������Ʈ ��ȿ�� �˻�
    private void ValidateComponents()
    {
        if (runeObj == null) Debug.Log("m_runeObj == null");
        if (runeSoundManager == null) Debug.Log("m_runeSoundManager == null");
        if (runeSight == null) Debug.Log("m_runeSight == null");
    }

    // �� ����
    private void RuneControl()
    {
        if (!RuneData.RuneUseControl) return;

        HandleRuneShoot(); // �� �߻� ó��
        HandleRuneReturn(); // �� ��ȯ ó��

        if (isShoot)
        {
            DecreaseBatteryWhileShooting();
            UpdateBatteryUI();
        }
    }

    private void DecreaseBatteryWhileShooting()
    {
        if (RuneData.RuneBattery > 0)
        {
            RuneData.RuneBattery -= batteryDrainSpeed * Time.deltaTime;
        }
        else
        {
            RunePowerLose();
            RuneData.RuneBattery = 0f;
        }
    }

    private void UpdateBatteryUI()
    {
        if (!isShoot)
            return;

        float batteryPercentage = RuneData.RuneBattery / 1000f;
        Color color;

        if (batteryPercentage > 0.75f)
            color = Color.green;
        else if (batteryPercentage > 0.25f)
            color = Color.yellow;
        else
            color = Color.red;

        if (batteryPercentage > 0)
        {
            // Start the coroutine to sequentially activate battery UI elements
            StartCoroutine(ActivateBatteryUISequentially(true, batteryPercentage));
        }

        // ��� SpriteRenderer ������Ʈ�� ���� ����
        foreach (var ui in batteryUI)
        {
            ui.DOColor(color, 5f);
        }
    }



    private IEnumerator ActivateBatteryUISequentially(bool isVisible, float batteryPercentage)
    {
        if (isVisible)
        {
            int visibleUIElements = 0;

            if (batteryPercentage > 0.75f)
            {
                visibleUIElements = 4;
            }
            else if (batteryPercentage > 0.50f)
            {
                visibleUIElements = 3;
                batteryUI[3].gameObject.SetActive(false);
            }
            else if (batteryPercentage > 0.25f)
            {
                visibleUIElements = 2;
                batteryUI[3].gameObject.SetActive(false);
                batteryUI[2].gameObject.SetActive(false);
            }
            else
            {
                visibleUIElements = 1;
                batteryUI[3].gameObject.SetActive(false);
                batteryUI[2].gameObject.SetActive(false);
                batteryUI[1].gameObject.SetActive(false);
            }

            for (int i = 0; i < visibleUIElements; i++)
            {
                batteryUI[i].gameObject.SetActive(isVisible);
                yield return new WaitForSeconds(0.2f);  // Adjust delay as needed
            }
        }
        else
        {
            foreach (var ui in batteryUI)
            {
                ui.gameObject.SetActive(isVisible);
                yield return new WaitForSeconds(0.2f);  // Adjust delay as needed
            }
        }
    }

    // �� �̵�
    private void RuneMove()
    {
        // ���� ��ǥ ���������� �̵��� ó��
        MoveRuneToTarget();
    }

    private void MoveRuneToTarget()
    {
        runeObj.transform.DOMove(target, moveTime);
    }

    // �� �߻� ó��


    private void HandleMouseClicksForShooting()
    {
        if (Input.GetMouseButtonDown(1) && !isMouseDelay && isShoot)
        {
            PlayRuneOffSoundAndDelayMouse();
            isShoot = false;
            SetBatteryUIVisible(false, RuneData.RuneBattery / 1000f);
        }
        else if (Input.GetMouseButtonDown(0) && !isMouseDelay)
        {
            ChangeTargetToMouse();
            PlayRuneOnSoundAndDelayMouse();
            HandleShootingWithBattery();
        }
        else if (!isShoot)
        {
            ReturnRune();
        }
    }

    private void PlayRuneOffSoundAndDelayMouse()
    {
        runeSoundManager.PlayRuneOff();
        isMouseDelay = true;
        StartCoroutine("MouseClickDelay");
    }

    private void PlayRuneOnSoundAndDelayMouse()
    {
        runeSoundManager.PlayRuneOn();
        isMouseDelay = true;
        StartCoroutine("MouseClickDelay");
    }

    private void HandleShootingWithBattery()
    {
        RuneData.RuneActive = true;
        RuneData.RuneLightArea.enabled = true;

        if (isShoot && RuneData.RuneBattery > 0)
        {
            RuneData.RuneBattery -= additionalMovementReduction;
        }

        isShoot = true;
        SetBatteryUIVisible(true, RuneData.RuneBattery / 1000f);
    }


    // ���콺 ��ġ�� ��ǥ ����
    private void ChangeTargetToMouse()
    {
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    // �� ��ȯ
    private void ReturnRune()
    {
        if (transform.rotation.y == 0)
        {
            target = new Vector2(transform.position.x + runePosition.x, transform.position.y + runePosition.y);
        }
        else if (transform.rotation.y != 0f)
        {
            target = new Vector2(transform.position.x + (-runePosition.x), transform.position.y + runePosition.y);
        }
        RuneData.RuneActive = false;
        RuneData.RuneLightArea.enabled = false;
    }

    // �� �߻� ó��
    private void HandleRuneShoot()
    {
        // ���콺 Ŭ���� ���� �߻� �Ǵ� ���͸� UI ����� ó��
        HandleMouseClicksForShooting();
    }

    private void SetBatteryUIVisible(bool isVisible, float batteryPercentage)
    {
        batteryBorder.gameObject.SetActive(isVisible);
        StartCoroutine(ActivateBatteryUISequentially(isVisible, batteryPercentage));
    }

    // �� ��ȯ ó��
    private void HandleRuneReturn()
    {
        if (RuneIsFarEnough())
        {
            StopShooting();
        }
    }

    private bool RuneIsFarEnough()
    {
        return Vector2.Distance(runeObj.transform.position, transform.position) >= RuneReturnDistance();
    }

    private void StopShooting()
    {
        isShoot = false;
        isMouseDelay = true;
        runeSoundManager.PlayRuneOff();
        StartCoroutine(MouseClickDelay());
    }

    // �� �Ŀ� ����
    public void RunePowerLose()
    {
        DecreaseRunePowerOverTime(0f, RuneData.RunePowerBackTime);
    }

    // �� �Ŀ� ȸ��
    public void RunePowerBack()
    {
        RuneData.RuneLightArea.enabled = true;
        DecreaseRunePowerOverTime(RuneData.RuneOuterRadius, RuneData.RunePowerBackTime);
    }

    private void DecreaseRunePowerOverTime(float targetRadius, float duration)
    {
        DOTween.To(() => runeSight.pointLightOuterRadius, x => runeSight.pointLightOuterRadius = x, targetRadius, duration);
    }


    // �� ��ȯ �Ÿ� ����
    private float RuneReturnDistance()
    {
        return runeReturnDistance;
    }

    // �� �̵� ����
    public void RuneStop()
    {
        target = runeObj.transform.position;
    }

    // ���콺 Ŭ�� ���� �ڷ�ƾ
    private IEnumerator MouseClickDelay()
    {
        yield return new WaitForSeconds(shootDelayTime);
        ResetMouseDelay();
    }

    private void ResetMouseDelay()
    {
        isMouseDelay = false;
    }

    // �� �浹 ó��
    private void RuneColliding()
    {
        RaycastHit2D hit = GetRaycastHit();

        if (HitObjectIsCollidable(hit))
        {
            target = hit.point;
        }
    }

    private RaycastHit2D GetRaycastHit()
    {
        return Physics2D.Raycast(runeObj.transform.position,
            GetDirectionToTarget(),
            GetDistanceToTarget(),
            layerMask);
    }

    private Vector2 GetDirectionToTarget()
    {
        return (target - (Vector2)runeObj.transform.position).normalized;
    }

    private float GetDistanceToTarget()
    {
        return Vector2.Distance(runeObj.transform.position, target);
    }

    private bool HitObjectIsCollidable(RaycastHit2D hit)
    {
        return hit.collider != null &&
               (hit.collider.gameObject.layer == (int)LayerName.Platform ||
                hit.collider.gameObject.layer == (int)LayerName.Wall ||
                hit.collider.gameObject.layer == (int)LayerName.RiskFactor);
    }

    private bool hasRestarted = false;
    public void RuneCharging()
    {
        if (isCharge)
        {
            if (!hasRestarted)
            {
                runeCharge.Play();
                hasRestarted = true;
            }
        }
        else
        {
            runeCharge.Stop();
            hasRestarted = false;  // ��ƼŬ�� ���߸� �ٽ� ������ �� �ֵ��� �÷��׸� �����մϴ�.
        }
    }
}
