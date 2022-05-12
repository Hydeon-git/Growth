using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanviEscena : MonoBehaviour
{
    public float counter;
    	
	void Update ()
    {
        counter += Time.deltaTime;

		if (counter > 74f)
        {
            SceneManager.LoadScene("GrowthGame");
        }
        if (Input.anyKey)
        {
            SceneManager.LoadScene("GrowthGame");
        }
    }
}
