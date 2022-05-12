using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    public float platformMovementSpeed = 4f;
    public float remainingTime;
    private float yDirection = -1f;

    private void Update()
    {
        transform.Translate(Vector2.up * yDirection * platformMovementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TrapDisabler")
        {
            StartCoroutine(StopPlatform(remainingTime));

        }
        else if (collision.gameObject.tag == "TrapEnabler")
        {
            StartCoroutine(StopPlatform(remainingTime));
        }
    }

    private IEnumerator StopPlatform(float seconds)
    {
        float speed = platformMovementSpeed;
        platformMovementSpeed = 0f;
        yield return new WaitForSeconds(seconds);
        platformMovementSpeed = speed;
        yDirection *= -1;
    }
}