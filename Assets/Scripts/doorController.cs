using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    public Animator anim;

    public GameObject Switch;

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isOpen", Switch.GetComponent<switchController>().isOpen);
    }
}
