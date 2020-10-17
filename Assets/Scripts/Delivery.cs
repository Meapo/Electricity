using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Delivery : MonoBehaviour
{
    private AudioSource music;
    private AudioClip nextLevel;
    private AudioClip victory;

    public Animator anim;
    public float transitionTime = 1f;
    public Transform player1;
    public Transform player2;
    public float passDistance = 1f;
    float dis1, dis2;

    private void Awake()
    {
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        nextLevel = Resources.Load<AudioClip>("Music/nextLevel");
        victory = Resources.Load<AudioClip>("Music/victory");
    }

    // Update is called once per frame
    void Update()
    {
        dis1 = Vector2.Distance(player1.position, transform.position);
        dis2 = Vector2.Distance(player2.position, transform.position);
        if (dis1 < passDistance&& dis2 < passDistance)
        {
            StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex));
        }
    }

    IEnumerator LoadNextLevel(int index)
    {
        anim.SetTrigger("end");
        if (!music.isPlaying)
        {
            music.clip = nextLevel;
            music.Play();
        }
        
        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(index + 1);
    }

}
