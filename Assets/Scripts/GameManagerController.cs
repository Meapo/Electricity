using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerController : MonoBehaviour
{
    protected AudioSource music;
    protected AudioClip death;
    protected AudioClip Boom;
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
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        death = Resources.Load<AudioClip>("Music/death1");
        Boom = Resources.Load<AudioClip>("Music/boom2");
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
        if (!music.isPlaying)
        {
            music.clip = death;
            music.Play();
        }
        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        anim.SetTrigger("end");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
