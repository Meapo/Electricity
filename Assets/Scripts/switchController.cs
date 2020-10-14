using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchController : MonoBehaviour
{
    public SpriteRenderer open;
    public SpriteRenderer close;
    public bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = open.sprite;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = close.sprite;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="relay")
        {
            isOpen = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "relay")
        {
            isOpen = false;
        }
    }
}
