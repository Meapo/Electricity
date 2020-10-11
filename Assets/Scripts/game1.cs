using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game1 : MonoBehaviour
{
    public Animator anim;
    public float transitionTime = 1f;

    public List<GameObject> trapGrounds;

    public Transform player1born;
    public Transform player2born;

    public GameObject Player1;
    public GameObject Player2;

    private bool isInit = true;

    static public game1 instance;
    private void Awake()
    {
        if (instance!=null)
        {
            Debug.Log("Find other gamemanager instance.");
        }
        else
        {
            instance = this;
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
            Instantiate(Player1, player1born.position, player1born.rotation);
            Instantiate(Player2, player2born.position, player2born.rotation);
            foreach (var item in trapGrounds)
            {
                item.GetComponent<trapBreak>().BreakTrap();
            }
            isInit = false;
        }
    }

    public void Die()
    {
        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        anim.SetTrigger("end");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
