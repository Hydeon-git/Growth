using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    public bool facingRight;
    public bool facingLeft;
    public float platformMovementSpeed = 4f;

    
    private void FixedUpdate()
    {
        if (facingRight)
        {
            transform.position = new Vector2(transform.position.x + platformMovementSpeed * Time.deltaTime, transform.position.y);
        }
        else if (facingLeft)
        {
            transform.position = new Vector2(transform.position.x - platformMovementSpeed * Time.deltaTime, transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("PlatformPos1"))
        {
            facingRight = false;
            facingLeft = true;
        }
        if (collision.gameObject.tag.Equals("PlatformPos2"))
        { 
            facingLeft = false;
            facingRight = true;
        }
    }
}
