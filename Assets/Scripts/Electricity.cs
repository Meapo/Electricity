using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    public SpriteRenderer player1;
    public SpriteRenderer player2; 

    // 一定要确保第一个是player1，第二个是player2，后面的是relay
    public List<Transform> transforms;
    private int itemCount;

    List<List<float>> distanceMatrix = new List<List<float>>();
    
    List<int> link;
    float minLength;

    private float maxDistance = 3.3f;

    public float maxLifeTime = 3.0f;
    private float LifeTime;
    private GameManagerController instance;
    private game1 game1Instance;

    private ChainLightning chainLightning;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        LifeTime = maxLifeTime;
        instance = GameManagerController.instance;
        game1Instance = game1.game1Instance;
        chainLightning = GetComponent<ChainLightning>();
        lineRenderer = GetComponent<LineRenderer>();
        // 初始化距离矩阵
        itemCount = transforms.Count;
        if (itemCount < 2)
        {
            Debug.LogError("Transform list is not signed correctly.");
        }
        else
        {
            initDistanceMatrix();
        }
    }

    // Update is called once per frame
    void Update()
    {
        player1.color = new Color(player1.color.r, player1.color.g, player1.color.b, LifeTime / maxLifeTime);
        player2.color = new Color(player2.color.r, player2.color.g, player2.color.b, LifeTime / maxLifeTime);
    }

    private void FixedUpdate()
    {
        updateDistanceMatrix();
        link = new List<int>();
        if (distanceMatrix[0][1]<=maxDistance)
        {
            link.Add(0); link.Add(1);
        }
        else
        {
            minLength = float.MaxValue;
            List<int> temp = new List<int>();
            minDistance(temp, 0, 1, 0f);
        }

        if (link.Count==0)
        {
            LifeTime -= Time.fixedDeltaTime;
            // 断开连接
            chainLightning.StartPosition = null;
            chainLightning.EndPostion = null;
            lineRenderer.enabled = false;
        }
        else
        {
            LifeTime = maxLifeTime;
            // 连接电流
            lineRenderer.enabled = true;
            chainLightning.StartPosition = transforms[link[0]];
            chainLightning.EndPostion = new List<Transform>();
            for (int i = 1; i < link.Count; i++)
            {
                chainLightning.EndPostion.Add(transforms[link[i]]);
            }

        }
        if (LifeTime <= 0f)
        {
            if (instance!=null)
            {
                instance.Die();
            }
            else if (game1Instance!=null)
            {
                game1Instance.Die();
            }
            else
            {
                Debug.LogError("No game manager instance.");
            }
            //LifeTime = maxLifeTime;
            
        }
    }

    void initDistanceMatrix()
    {
        
        for (int j = 0; j < itemCount; j++)
        {
            List<float> item = new List<float>();
            for (int i = 0; i < itemCount; i++)
            {
                item.Add(float.MaxValue);
            }
            distanceMatrix.Add(item);
        }
    }

    void updateDistanceMatrix()
    {
        for (int i = 0; i < itemCount - 1; i++)
        {
            for (int j = i + 1; j < itemCount; j++)
            {
                float distance = Vector2.Distance(transforms[i].position, transforms[j].position);
                if (distance > maxDistance)
                {
                    distance = float.MaxValue;
                }
                distanceMatrix[i][j] = distance;
                distanceMatrix[j][i] = distance;
            }
        }
    }

    void minDistance(List<int> route, int cur, int end, float length)
    {
        route.Add(cur);
        if (route[0]!=0)
        {
            Debug.LogError("wtf: " + route[0]);
        }
        if (cur==end)
        {
            if (length < minLength)
            {
                List<int> temp = new List<int>(route);
                link = temp;
                minLength = length;
            }
        }
        else
        {
            for (int i = 0; i < itemCount; i++)
            {
                if (distanceMatrix[cur][i]<=maxDistance)
                {
                    float temp = distanceMatrix[cur][i];
                    distanceMatrix[i][cur] = distanceMatrix[cur][i] = float.MaxValue;
                    minDistance(new List<int>(route), i, end, length + temp);
                    distanceMatrix[i][cur] = distanceMatrix[cur][i] = temp;
                }
            }
        }
        route.Remove(cur);
    }

}
