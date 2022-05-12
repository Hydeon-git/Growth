using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCanvas : MonoBehaviour
{
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public GameManager GameManager;

    private void Update()
    {
        if (GameManager.GaveLives == 2)
        {
            heart3.gameObject.SetActive(false);
        }
        if (GameManager.GaveLives == 1)
        {
            heart2.gameObject.SetActive(false);
        }
        if (GameManager.GaveLives == 0)
        {
            heart1.gameObject.SetActive(false);
        }
    }
}
