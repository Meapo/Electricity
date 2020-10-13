using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerController : MonoBehaviour
{
    public Animator anim;
    public float transitionTime = 1f;

    public List<GameObject> trapGrounds;

    public GameObject Player1;
    public GameObject Player2;

    static public GameManagerController instance;
    private void Awake()
    {
        if (instance != null)
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
