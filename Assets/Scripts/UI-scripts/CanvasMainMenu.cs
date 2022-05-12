using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMainMenu : MonoBehaviour
{
    public GameObject canvasSettings;
    public GameObject canvasCredits;
    public GameObject canvasOptions;
    public GameObject canvasUI;
    public GameObject canvasCredits_bkg;


    public void PlayButton()
    {
        SceneManager.LoadScene("AnimacioInicial");
    }
    public void SettingsButton()
    {
        canvasOptions.SetActive(true);
        canvasSettings.SetActive(false);
        gameObject.SetActive(false);
        canvasCredits.SetActive(false);
    }
    public void CreditsButton()
    {
        canvasCredits.SetActive(true);
        gameObject.SetActive(false);
        canvasSettings.SetActive(false);
        canvasUI.SetActive(false);
        canvasCredits_bkg.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
