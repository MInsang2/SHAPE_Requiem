using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] public ArrowScript arrowPrefab;
    [SerializeField] public float arrowSpeed;
    [SerializeField] private float time = 0;
    [SerializeField] public float arrowDelay;

    public bool formLeft = false;
    public bool shoot;

    private Vector2 dir2;

    void Start()
    {
        dir2 = new Vector2(arrowSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot == true)
        {
            if (time < Time.time)
            {
                ArrowScript arrow = Instantiate(arrowPrefab, transform.position, transform.rotation) as ArrowScript;
                arrow.dir = dir2;
                time = Time.time + arrowDelay;
            }
        }
    }
}
