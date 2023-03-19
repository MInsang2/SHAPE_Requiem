using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] public Transform firepoint;
    [SerializeField] public GameObject Arrow;
    [SerializeField] public float ArrowSpeed = 1;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(ArrowSpeed);
            Instantiate(Arrow, firepoint.position, firepoint.rotation);
            Destroy(gameObject);
        }
    }
}
