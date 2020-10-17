using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public GameObject BGM;

    GameObject thisBGM;
    // Start is called before the first frame update
    void Start()
    {
        thisBGM = GameObject.FindGameObjectWithTag("BGM");
        if (thisBGM==null)
        {
            thisBGM = Instantiate(BGM);
        }
    }

    
}
