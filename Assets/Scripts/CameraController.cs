using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float smoothSpeed = 5f;
    public float transDistance = 10f;
    public float ViewCoef = 1.5f;
    private float initView;
    private Vector3 midPoint;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        midPoint = (player1.position + player2.position) / 2;
        offset = new Vector3(0f, 0f, transform.position.z);
        initView = gameObject.GetComponent<Camera>().fieldOfView;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float currentView = gameObject.GetComponent<Camera>().fieldOfView;
        if (Vector3.Distance(player1.position, player2.position) > transDistance)
        {
            gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(currentView, initView * ViewCoef, smoothSpeed * Time.deltaTime);
        }
        else
        {
            gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(currentView, initView, smoothSpeed * Time.deltaTime);
        }

        
        midPoint = (player1.position + player2.position) / 2;
        transform.position =  Vector3.Lerp(transform.position, midPoint+offset, smoothSpeed*Time.deltaTime);
    }
}
