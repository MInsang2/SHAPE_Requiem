using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour
{
    [SerializeField] ArrowScript arrow;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            arrow.isActive = true;
            Destroy(gameObject);
        }
    }
}
