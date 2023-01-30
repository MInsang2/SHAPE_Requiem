using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{




    public void StartClick()
    {
        SceneManager.LoadScene("underground_MinSang");
    }

    public void ExitClick()
    {
        Application.Quit();
    }
}
