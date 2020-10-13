using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    // 一定要确保第一个是player1，第二个是player2，后面的是relay
    public List<Transform> transforms;
    private int itemCount;

    List<List<float>> distanceMatrix = new List<List<float>>();

    private float LifeTime = 3.0f;
    private GameManagerController instance;
    private game1 game1Instance;

    private ChainLightning chainLightning;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
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

    }

    private void FixedUpdate()
    {
        updateDistanceMatrix();
        List<int> link = new List<int>();
        minDistance(link, 0, 1);
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
            LifeTime = 3.0f;
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
            
            
        }
    }

    void initDistanceMatrix()
    {
        
        for (int j = 0; j < itemCount; j++)
        {
            List<float> item = new List<float>();
            for (int i = 0; i < itemCount; i++)
            {
                item.Add(0f);
            }
            distanceMatrix.Add(item);
        }
    }

    void updateDistanceMatrix()
    {
        for (int i = 0; i < itemCount - 1; i++)
        {
            for (int j = i; j < itemCount; j++)
            {
                float distance = Vector2.Distance(transforms[i].position, transforms[j].position);
                if (distance > 3f)
                {
                    distance = float.MaxValue;
                }
                distanceMatrix[i][j] = distance;
                distanceMatrix[j][i] = distance;
            }
        }
    }

    void minDistance(List<int> result, int cur, int end)
    {
        result.Add(cur);
        if (cur==end)
        {
            return;
        }
        else
        {
            int next = findMinDistance(cur);
            if (next==-1)
            {
                result.Clear();
                return;
            }
            else
            {
                float tempDistance = distanceMatrix[cur][next];
                distanceMatrix[cur][next] = distanceMatrix[next][cur] = float.MaxValue;
                minDistance(result, next, end);
                distanceMatrix[cur][next] = distanceMatrix[next][cur] = tempDistance;
            }
        }
    }

    int findMinDistance(int ind)
    {
        float min = float.MaxValue;
        int result = -1;
        for (int i = 0; i < itemCount; i++)
        {
            if (i!=ind)
            {
                if (distanceMatrix[i][ind] < min)
                {
                    min = distanceMatrix[i][ind];
                    result = i;
                }
            }
        }
        return result;
    }
}
