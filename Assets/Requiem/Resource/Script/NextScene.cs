using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string scenename;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            SceneManager.LoadScene(scenename);
        }
    }
}
