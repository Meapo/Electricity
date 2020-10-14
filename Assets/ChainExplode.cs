using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainExplode : MonoBehaviour
{
    public List<GameObject> trapGrounds;
    public GameObject Switch;
    public float gapTime = 1f;
    private bool hasExplode = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
        item.GetComponent<Crasher>().Crash();
        item.SetActive(false);
    }
}
