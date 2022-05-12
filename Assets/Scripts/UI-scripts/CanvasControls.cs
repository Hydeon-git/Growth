using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControls : MonoBehaviour
{
    public GameObject canvasOptions;

    public void Back()
    {
        canvasOptions.SetActive(true);
        gameObject.SetActive(false);
    }
}
