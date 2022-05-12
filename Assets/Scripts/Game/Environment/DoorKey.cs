using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorKey : MonoBehaviour
{
    public GameObject Gave;
    public float OpenDoorSpeed;
    public bool CanOpenDoor;
    public bool DoorIsOpen;
    public Text OpenDoorKeyText;
    public int DoorMovement = -5;

    private void Update()
    {
        if (!DoorIsOpen && CanOpenDoor && Gave.GetComponent<PlayerController>().currentKeys > 0 && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(OpenDoorCoroutine());
        }
    }
    public void OpenDoor()
    {
        StartCoroutine(OpenDoorCoroutine());
    }
    public IEnumerator OpenDoorCoroutine()
    {
        float targetY = transform.position.y + DoorMovement;
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
    // Can open door = true
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && Gave.GetComponent<PlayerController>().currentKeys > 0)
        {
            CanOpenDoor = true;
            OpenDoorKeyText.gameObject.SetActive(true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CanOpenDoor = false;
        OpenDoorKeyText.gameObject.SetActive(false);
    }
}
