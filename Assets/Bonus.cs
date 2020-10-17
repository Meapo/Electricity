using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public GameObject relay;
    public float gapTime = 0.3f;
    private float waitTime = 0f;
    public float xRange = 6.0f;
    public int itemNum = 50;

    // Update is called once per frame



    private void Start()
    {
        for (int i = 0; i < itemNum; i++)
        {
            waitTime += gapTime;
            StartCoroutine(Generate());
        }
    }

    IEnumerator Generate()
    {
        
        yield return new WaitForSeconds(waitTime);
        float x = Random.Range(-xRange, xRange);
        GameObject item = Instantiate(relay, new Vector3(x, 5f, 0f), Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-180f, 180f))));
        float scale = Random.Range(0.5f, 1f);
        item.transform.localScale = new Vector2(scale, scale);
    }

}
