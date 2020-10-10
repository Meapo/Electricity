using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3.0f;

    public float jumpSpeed = 5.0f;

    public float jumpAttenuation = 10f;

    public Transform judgePoint;

    public Transform groundJudgePoint;

    private float groundJudgeOffset;

    protected float h = 0f;

    protected bool isGround = false;

    protected bool isPressed = false;

    protected bool isFacingRight = false;

    protected bool StopLeft = false;

    protected bool StopRight = false;

    protected Rigidbody2D rigid;

    public BoxCollider2D boxCollider;

    public LayerMask player;

    public LayerMask ground;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundJudgeOffset = transform.position.y - groundJudgePoint.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        isPressedJump();
        pressMove();
        
    }

    private void FixedUpdate()
    {
        // 判断左右是否有物体阻挡
        Collider2D[] colliders = Physics2D.OverlapBoxAll(judgePoint.position, new Vector2(boxCollider.size.x * transform.localScale.x, 0f), 0f, player);
        foreach (Collider2D item in colliders)
        {
            if (item.gameObject!=gameObject)
            {
                Debug.Log(item.gameObject.transform.position);
                //if (gameObject.transform.position.x > )
                //{

                //}
            }
        }
        Move();
        Jump(); 
        if (!isGround)
        {
            float velocityY = rigid.velocity.y - jumpAttenuation * Time.fixedDeltaTime;
            rigid.velocity = new Vector2(rigid.velocity.x, velocityY);
        }
        else
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0f);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    int count = collision.contactCount;
    //    if (collision.GetContact(0).normal.x == 1)
    //    {
    //        StopLeft = true;
    //    }
    //    else if (collision.GetContact(0).normal.x == -1)
    //    {
    //        StopRight = true;
    //    }
    //    else
    //    {
    //        StopLeft = false;
    //        StopRight = false;
    //    }
    //}




    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundJudgePoint.position, new Vector2(0f, boxCollider.size.y * transform.localScale.y - groundJudgeOffset), 0f, ground | player);
        if (colliders.Length==1)
        {
            isGround = false;
            anim.SetBool("isGround", isGround);
        }
        foreach (var item in colliders)
        {
            if (item.gameObject!=gameObject)
            {
                if (isGround && item)
                {
                    anim.SetFloat("speed", Mathf.Abs(h));
                }
            }
        }
        
        rigid.velocity = new Vector2(h * moveSpeed, rigid.velocity.y);
        
    }

    private void Jump()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundJudgePoint.position, new Vector2(0f, boxCollider.size.y * transform.localScale.y - groundJudgeOffset), 0f, ground | player);
        foreach (var item in colliders)
        {
            if (item.gameObject!=gameObject)
            {
                if ((!isGround) && item)
                {
                    isGround = true;
                    anim.SetBool("isGround", isGround);
                }
            }
        }
        


        if (isGround && isPressed)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
            isGround = false;
            anim.SetBool("isGround", isGround);
            anim.SetTrigger("jump");
            isPressed = false;
        }
    }

    virtual protected void isPressedJump()
    {

    }

    virtual protected void pressMove()
    {

    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(judgePoint.position, new Vector2(boxCollider.size.x * transform.localScale.x, 0f));
    }
}
