using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainExplode : MonoBehaviour
{
    private AudioSource music;
    private AudioClip Boom;
    public List<GameObject> trapGrounds;
    public GameObject Switch;
    public float gapTime = 1f;
    private bool hasExplode = false;

    private void Awake()
    {
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        Boom = Resources.Load<AudioClip>("Music/boom2");
    }

    // Update is called once per frame
    void Update()
    {
        if (Switch.GetComponent<switchController>().isOpen&&!hasExplode)
        {
            hasExplode = true;
            float waitTime = 0f;
            foreach (var item in trapGrounds)
            {
                waitTime += gapTime;
                StartCoroutine(explode(item, waitTime));
            }
        }
        
    }

    IEnumerator explode(GameObject item, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        music.clip = Boom;
        music.Play();
        item.GetComponent<Crasher>().Crash();
        item.SetActive(false);
    }
}
