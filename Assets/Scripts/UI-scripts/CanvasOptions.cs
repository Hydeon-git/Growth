using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOptions : MonoBehaviour
{
    public GameObject canvasConfiguration;
    public GameObject canvasControls;
    public GameObject canvasMainMenu;
    
	public void ConfigurationButton()
    {
        canvasConfiguration.SetActive(true);
        gameObject.SetActive(false);
        canvasControls.SetActive(false);
	}

    public void ControlsButton()
    {
        canvasConfiguration.SetActive(false);
        gameObject.SetActive(false);
        canvasControls.SetActive(true);
    }

    public void Back()
    {
        gameObject.SetActive(false);
        canvasMainMenu.SetActive(true);
        canvasControls.SetActive(false);
    }
}
