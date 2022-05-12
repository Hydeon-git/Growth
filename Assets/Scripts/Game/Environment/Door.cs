using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float OpenDoorSpeed;    
    
    public void OpenDoor()
    {
        StartCoroutine(OpenDoorCoroutine());
    }
    public void CloseDoor()
    {
        StartCoroutine(CloseDoorCoroutine());
    }
    public IEnumerator OpenDoorCoroutine()
    {
        float targetY = transform.position.y - 9f;
        float startY = transform.position.y;
        float i = 0f;
        Vector3 position = transform.position;

        while (i < 2f)
        {
            i += Time.deltaTime;
            position.y = Mathf.Lerp(startY, targetY, i / 2f);
            transform.position = position;
            yield return null;
        }
    }
    public IEnumerator CloseDoorCoroutine()
    {
        float targetY = transform.position.y + 9f;
        float startY = transform.position.y;
        float i = 0f;
        Vector3 position = transform.position;

        while (i < 2f)
        {
            i += Time.deltaTime;
            position.y = Mathf.Lerp(startY, targetY, i / 2f);
            transform.position = position;
            yield return null;
        }
    }   
}

