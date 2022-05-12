using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreMenu : MonoBehaviour
{
    public GameObject canvasMenu;
    public GameObject canvasPreMenu;
	
	void Update ()
    {
        if(Input.anyKey)
        {
            canvasMenu.SetActive(true);
            canvasPreMenu.SetActive(false);
        }
	}
}
