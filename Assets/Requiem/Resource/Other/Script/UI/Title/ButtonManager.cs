using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{




    public void StartClick()
    {
        SceneManager.LoadScene("underground_map1");
    }

    public void ExitClick()
    {
        Application.Quit();
    }
}
