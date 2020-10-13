using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game1 : GameManagerController
{

    private bool isInit = true;

    static public game1 game1Instance;
    private void Awake()
    {
        if (game1Instance!=null)
        {
            Debug.Log("Find other gamemanager instance.");
        }
        else
        {
            game1Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInit&&Input.anyKeyDown)
        {
            Player1.SetActive(true);
            Player2.SetActive(true);
            foreach (var item in trapGrounds)
            {
                item.GetComponent<trapBreak>().BreakTrap();
            }
            isInit = false;
        }
    }
}
