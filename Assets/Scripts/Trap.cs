using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public LayerMask player;

    private game1 instance;

    private void Start()
    {
        instance = game1.instance;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject item = collision.gameObject;
        int layer = 1 << item.layer;
        if (layer==player.value)
        {
            Debug.Log("Player die");
            instance.Die();
        }
    }
}
