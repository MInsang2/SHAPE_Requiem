using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtual;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtual = GetComponent<CinemachineVirtualCamera>();
        target = GameObject.Find("Player").transform;
        cinemachineVirtual.Follow = target;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
