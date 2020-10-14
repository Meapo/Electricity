using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public LayerMask player;

    private GameManagerController instance;
    private game1 game1Instance;
    private void Start()
    {
        instance = GameManagerController.instance;
        game1Instance = game1.game1Instance;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject item = collision.gameObject;
        int layer = 1 << item.layer;
        if (layer==player.value)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            if (instance!=null)
            {
                instance.Die();
            }
            else
            {
                game1Instance.Die();
            }
        }
    }
}
