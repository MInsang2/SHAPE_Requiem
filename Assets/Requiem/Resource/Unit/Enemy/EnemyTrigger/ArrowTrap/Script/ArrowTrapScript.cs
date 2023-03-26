using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrapScript : RiskFactor
{
    [SerializeField] Transform firepoint;
    [SerializeField] GameObject Arrow;
    [SerializeField] float ShootingSpeed = 0;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while(true)
        {
            yield return new WaitForSeconds(ShootingSpeed);
            Instantiate(Arrow, firepoint.position, firepoint.rotation);
        }
    }
}
