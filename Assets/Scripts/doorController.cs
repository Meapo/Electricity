using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    private AudioSource music;
    private AudioClip open;
    public Animator anim;

    public GameObject Switch;


    private void Awake()
    {
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        open = Resources.Load<AudioClip>("Music/doorOpen");
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isOpen", Switch.GetComponent<switchController>().isOpen);
    }

    public void openDoorAudioPlay()
    {
        music.clip = open;
        music.Play();
    }
}
