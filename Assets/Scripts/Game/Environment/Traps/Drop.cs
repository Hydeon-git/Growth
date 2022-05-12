using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public float dropSpeed = 15f;
    private GameObject Gave;
    private GameObject GameManager;

    private void Update()
    {
        transform.Translate(Vector2.down * dropSpeed* Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Ground"))
        {
            Destroy(gameObject);
        }
        if (collision.tag.Equals("Player"))
        {
            GameManager = GameObject.Find("GameManager");
            Destroy(gameObject);            
            if (GameManager.GetComponent<GameManager>().GaveLives > 0)
            {
                GameManager.GetComponent<GameManager>().DownGaveLives();
            }
            if (GameManager.GetComponent<GameManager>().GaveLives < 1)
            {                
                collision.GetComponent<PlayerController>().KilledByDrop();
            }
        }
    }
}
