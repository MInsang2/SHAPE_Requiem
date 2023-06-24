using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using Cinemachine;

public class DivArea : AreaData
{
    Tilemap tilemap;
    [SerializeField] Collider2D cameraArea;
    CinemachineVirtualCamera mainCM;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        cameraArea = transform.GetChild(0).GetComponent<Collider2D>();
        cameraArea.transform.parent = null;
        mainCM = DataController.MainCM;


        tilemap.color = new Color(0f, 0f, 0f, 0f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ChangeCameraArea();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ChangeCameraArea();
        }
    }

    public void ChangeCameraArea()
    {
        mainCM.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = cameraArea;
    }
}
