using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject dustAnim;
    private GameObject createdDust;
    public Transform dustPoint;
    public float dustAnimTime = 3f;
    private bool isFirstGround = true;

    public float moveSpeed = 3.0f;

    public float jumpSpeed = 5.0f;

    public float jumpAttenuation = 10f;

    public float lengthCoef;
    [HideInInspector]
    public float judgeLength;
    [HideInInspector]
    public float judgeHeight;

    public Transform groundJudgePoint;

    public Transform pickupPoint;

    private float groundJudgeOffset;

    protected float h = 0f;
    [HideInInspector]
    public bool isGround = false;
    [HideInInspector]
    public bool isPressed = false;

    protected bool isFacingRight = false;

    protected GameObject pickupItem;
    [HideInInspector]
    public bool StopLeft = false;
    [HideInInspector]
    public bool StopRight = false;

    protected Rigidbody2D rigid;

    public BoxCollider2D boxCollider;
    public LayerMask player;
    public LayerMask ground;
    public LayerMask relay;
    public LayerMask trap;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundJudgeOffset = transform.position.y - groundJudgePoint.position.y + 0.1f;
        judgeLength = boxCollider.size.x * transform.localScale.x * lengthCoef;
        judgeHeight = boxCollider.size.y * transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // 判断左右是否有player/relay阻挡
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(judgeLength, boxCollider.size.y * transform.localScale.y - 0.15f), 0f);
        if (colliders.Length == 1)
        {
            StopRight = false;
            StopLeft = false;
        }
        else
        {
            StopRight = false;
            StopLeft = false;
            foreach (var item in colliders)
            {
                if (item.gameObject.tag != "Tilemap" && item.gameObject != gameObject)
                {
                    if (transform.position.x - item.gameObject.transform.position.x > 0)
                    {
                        StopLeft = true;
                    }
                    else
                    {
                        StopRight = true;
                    }
                }
                else if (item.gameObject.tag == "Tilemap")
                {
                    ContactPoint2D[] points = new ContactPoint2D[16];
                    boxCollider.GetContacts(points);
                    if (points != null)
                    {
                        foreach (var point in points)
                        {
                            if (point.normal.x == -1)
                            {
                                StopRight = true;
                            }
                            else if (point.normal.x == 1)
                            {
                                StopLeft = true;
                            }
                        }

                    }


                }
            }
        }
        // 按键判断
        isPressedJump();
        pressMove();
        pressPickup();
       

    }

    private void FixedUpdate()
    {
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

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundJudgePoint.position, new Vector2(judgeLength/lengthCoef-0.1f, boxCollider.size.y * transform.localScale.y - groundJudgeOffset), 0f, ~trap);
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
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundJudgePoint.position, new Vector2(judgeLength / lengthCoef - 0.1f, boxCollider.size.y * transform.localScale.y - groundJudgeOffset), 0f, ~trap);
        foreach (var item in colliders)
        {
            if (item.gameObject!=gameObject)
            {
                if ((!isGround) && item)
                {
                    isGround = true;
                    anim.SetBool("isGround", isGround);
                    if (isFirstGround&&dustAnim!=null)
                    {
                        //createdDust = Instantiate(dustAnim, dustPoint.position, dustPoint.rotation);
                        createdDust = Instantiate(dustAnim, dustPoint);
                        StartCoroutine(destoryDust());
                        isFirstGround = false;
                    }
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

    virtual protected void pressPickup()
    {

    }

    protected void pickup()
    {
        if (isGround)
        {
            
            // 若有多个继电器则选择最近的一个
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(judgeLength, 0f), 0f, relay);
            float min = float.MaxValue;
            Collider2D closestCollider = null;
            foreach (var item in colliders)
            {
                float dist = Vector2.Distance(item.gameObject.transform.position, gameObject.transform.position);
                if (dist < min)
                {
                    closestCollider = item;
                    min = dist;
                }
            }
            // pick up closest collider
            if (closestCollider!=null)
            {
                pickupItem = closestCollider.gameObject;
                Destroy(pickupItem.GetComponent<Rigidbody2D>());
                pickupItem.GetComponent<BoxCollider2D>().enabled = false;
                pickupItem.transform.position = pickupPoint.position;
                pickupItem.transform.SetParent(pickupPoint);
            }
        }
    }

    protected void drop()
    {
        pickupItem.GetComponent<BoxCollider2D>().enabled = true;
        Rigidbody2D itemRigid = pickupItem.AddComponent<Rigidbody2D>();
        itemRigid.freezeRotation = true;
        pickupItem.transform.SetParent(null);
        pickupItem = null;
    }

    IEnumerator destoryDust()
    {
        yield return new WaitForSeconds(dustAnimTime);
        Destroy(createdDust);
    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(boxCollider.size.x * transform.localScale.x * lengthCoef, 0f));
    }
}
