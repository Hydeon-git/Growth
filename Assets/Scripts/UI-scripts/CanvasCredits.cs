using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCredits : MonoBehaviour
{
    public GameObject canvasMainMenu;
    public GameObject canvasUI;
    public GameObject canvasCredits_bkg;

    public void Back()
    {
        Debug.Log("BACK");
        canvasMainMenu.SetActive(true);
        gameObject.SetActive(false);
        canvasCredits_bkg.SetActive(false);
        canvasUI.SetActive(true);
    }

}
