using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifterController : MonoBehaviour
{
    private float maxHeight;
    private float minHeight;
    public float heightRange;
    public float speed;
    public float pauseTime;
    private float curPauseTime = 0f;
    private bool shouldUp = true;

    private BoxCollider2D boxCollider;
    public GameObject player1;
    public GameObject player2;
    public float heightOffset = 0.1f;
    public float speedAttenuationCoef = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        minHeight = transform.position.y;
        maxHeight = minHeight + heightRange;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        curPauseTime -= Time.fixedDeltaTime;
        if (curPauseTime<=0f)
        {
            if (shouldUp)
            {
                Up();
            }
            else
            {
                Down();
            }
        }

        // 判断player是否在box内部,在内部则让其boxcollider失效，否则令其有效
        IsInBox(player1);
        IsInBox(player2);

    }


    void Up()
    {
        transform.position += new Vector3(0f, speed * Time.fixedDeltaTime, 0f);
        if (transform.position.y >= maxHeight)
        {
            curPauseTime = pauseTime;
            shouldUp = false;
        }
    }

    void Down()
    {
        transform.position -= new Vector3(0f, speed * Time.fixedDeltaTime, 0f);
        if (transform.position.y <= minHeight)
        {
            curPauseTime = pauseTime;
            shouldUp = true;
        }
    }

    void IsInBox(GameObject player)
    {
        float xDiff = transform.position.x - player.transform.position.x;
        float yDiff = transform.position.y - player.transform.position.y;
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (Mathf.Abs(xDiff)<(boxCollider.size.x + playerController.judgeLength)/2
            && Mathf.Abs(yDiff) < (boxCollider.size.y + playerController.judgeHeight + heightOffset) / 2
            && yDiff > 0 && playerController.isGround)
        {
            player.GetComponent<BoxCollider2D>().enabled = false;
            if (playerController.isPressed)
            {
                //先将player放到升降台上面，然后再给一个跳跃速度
                player.transform.position = new Vector3(player.transform.position.x, transform.position.y + (boxCollider.size.y + playerController.judgeHeight + 2 * heightOffset) / 2, player.transform.position.z);
                player.GetComponent<BoxCollider2D>().enabled = true;
                Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();
                rigid.velocity = new Vector2(rigid.velocity.x, playerController.jumpSpeed * speedAttenuationCoef);
            }
            
        }
        else
        {
            player.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}
