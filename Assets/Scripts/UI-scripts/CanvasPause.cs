using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasPause : MonoBehaviour
{
    public void Continue()
    {        
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
