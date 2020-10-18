using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preventStuckInGround : MonoBehaviour
{
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        rigid = GetComponent<Rigidbody2D>();
        ContactPoint2D[] contactPoints = new ContactPoint2D[20];
        if (rigid!=null)
        {
            rigid.GetContacts(contactPoints);
            foreach (var point in contactPoints)
            {
                if (point.collider != null)
                {
                    if (point.collider.gameObject.tag == "Tilemap" && Mathf.Abs(point.normal.x) == 1)
                    {
                        gameObject.transform.position += new Vector3(point.normal.x * 0.01f, 0f, 0f);
                    }
                }

            }
        }
        
    }
}
