using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beltController : MonoBehaviour
{
    public float translateSpeed = 3f;
    
    public bool isRight = true;

    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        BoxCollider2D collisionCollider = collision.gameObject.GetComponent<BoxCollider2D>();
        if (collision.gameObject.transform.position.y-transform.position.y >= (collisionCollider.size.y + boxCollider.size.y - 0.02f)/2)
        {
            if (isRight)
            {
                collision.gameObject.transform.Translate(translateSpeed * Vector2.right * Time.fixedDeltaTime, Space.World);
            }
            else
            {
                collision.gameObject.transform.Translate(translateSpeed * Vector2.left * Time.fixedDeltaTime, Space.World);
            }
        }
        
        
    }
}
