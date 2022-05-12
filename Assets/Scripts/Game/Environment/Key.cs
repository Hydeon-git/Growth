using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    private bool canPickUpAnItem;
    public Text PickUpText;
    public GameObject Gave;

    private void Start()
    {
        PickUpText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (canPickUpAnItem && Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }
    }
    private void PickUpItem()
    {
        Gave.GetComponent<PlayerController>().currentKeys++;
        Destroy(gameObject);
    }
    // Key detects the player and enables the text and the boolean to pick up the item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PickUpText.gameObject.SetActive(true);
            canPickUpAnItem = true;
        }
    }
    // Player leaves the trigger collision and sets the text and boolean to false
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PickUpText.gameObject.SetActive(false);
            canPickUpAnItem = false;
        }
    }
}
