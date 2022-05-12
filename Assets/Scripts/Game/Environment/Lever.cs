using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    public GameObject LeverOff;
    public GameObject LeverOn;
    public GameObject DoorKey;
    public GameObject Gave;

    public Text UseLeverText;
    public bool CanOpenDoor;
    public bool DoorIsOpen;
    

    private void Start()
    {
        // Setting the lever to off
        gameObject.GetComponent<SpriteRenderer>().sprite = LeverOff.GetComponent<SpriteRenderer>().sprite;

        // LeverText to off
        UseLeverText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (CanOpenDoor && !DoorIsOpen)
        {
            UseLeverAndOpenDoor();
        }
        else if(CanOpenDoor && DoorIsOpen)
        {
            UseLeverAndCloseDoor();
        }
    }
    private void UseLeverAndOpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = LeverOn.GetComponent<SpriteRenderer>().sprite;
            DoorKey.GetComponent<Door>().OpenDoor();
            DoorIsOpen = true;
        }        
    }
    private void UseLeverAndCloseDoor()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = LeverOff.GetComponent<SpriteRenderer>().sprite;
            DoorKey.GetComponent<Door>().CloseDoor();
            DoorIsOpen = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            CanOpenDoor = true;
            UseLeverText.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CanOpenDoor = false;
        UseLeverText.gameObject.SetActive(false);
    }
}
