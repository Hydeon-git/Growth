using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDownCamera : MonoBehaviour
{
    // PLAYER
    public GameObject Gave;

    // CAMERA
    public Camera MainCamera;
    public bool canLookUp;
    public bool canLookDown;
    public float timeToLookDown = 1f;
    public float timeToGoBackToUp = 3f;

    private void Start()
    {
        canLookDown = true;
        canLookUp = false;
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.S) && Gave.GetComponent<PlayerController>().characterCanMove && canLookDown)
        {
            StartCoroutine(LookDown(timeToLookDown));
        }        
    }
    IEnumerator LookDown(float time)
    {
        canLookDown = false;
        yield return new WaitForSeconds(time);
        MainCamera.GetComponent<CameraSmooth>().offset = new Vector3(0f, -2.7f, -12f);
        canLookUp = true;
        StartCoroutine(LookUpFromDown(timeToGoBackToUp));
    }
    IEnumerator LookUpFromDown(float time)
    {
        canLookUp = false;
        yield return new WaitForSeconds(time);
        MainCamera.GetComponent<CameraSmooth>().offset = new Vector3(0f, 0f, -12f);
        canLookDown = true;
    }
}
