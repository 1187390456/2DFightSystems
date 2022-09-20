using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 speedV2;//速度

    Vector2 slideV2;
    Rigidbody2D rb;
    Animator anim;
    public Transform groundCheck;
    public Transform wallCheck;

    bool isCanMove;
    bool isFacing = true;//true右，false左
    bool isCanJump;
    bool isTouchGround = false;
    bool isTouchWall;
    bool isSlide;

    float inputDirection;
    [SerializeField]
    float groundCheckRadius;
    public float distance = 0.37f;
    public float slideSpeed = -3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        slideV2 = new Vector2(1, 0);
    }
    private void Update()
    {
        CheckInput();
        CheckAnimation();
        CheckEnvironment();
        CheckMove();
        CheckFlip();
        CheckJump();
        CheckSlideWall();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + distance, wallCheck.position.y));
    }
    //检查输入
    void CheckInput()
    {
        inputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    //检查动画
    void CheckAnimation()
    {
        anim.SetBool("isRunning", isCanMove);
        anim.SetBool("isCanJump", isCanJump);
        anim.SetBool("isTouchGround", isTouchGround);
        anim.SetFloat("rbV", rb.velocity.y);
        anim.SetBool("isSlide", isSlide);
    }
    //检查环境
    void CheckEnvironment()
    {
        isTouchGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("Ground"));
        isTouchWall = Physics2D.Raycast(wallCheck.position, slideV2, distance, LayerMask.GetMask("Ground"));
    }
    //检查移动
    void CheckMove()
    {
        if (inputDirection != 0)
        {
            isCanMove = true;
        }
        else
        {
            isCanMove = false;
        }
    }
    //移动
    void Move()
    {
        rb.velocity = new Vector2(inputDirection * speedV2.x, rb.velocity.y);
    }
    //检查转向
    void CheckFlip()
    {
        if (isFacing && inputDirection < 0)
        {
            Flip();
        }
        else if (!isFacing && inputDirection > 0)
        {
            Flip();
        }
    }
    //转向
    void Flip()
    {
        if (isFacing)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            slideV2 = new Vector2(-1, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            slideV2 = new Vector2(1, 0);
        }
        isFacing = !isFacing;
    }
    //检查跳跃
    void CheckJump()
    {
        if (isTouchGround || isTouchWall)
        {
            isCanJump = true;
        }
        else
        {
            isCanJump = false;
        }
    }
    //跳跃
    void Jump()
    {
        if (isCanJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, speedV2.y);
        }
    }
    //检查滑墙
    void CheckSlideWall()
    {
        if (isTouchWall && !isTouchGround && rb.velocity.y < 0)
        {
            isSlide = true;
            if (rb.velocity.y > slideSpeed)
            {
                slideSpeed = rb.velocity.y;
            }
            rb.velocity = new Vector2(rb.velocity.x, slideSpeed);
        }
        if (isTouchGround || !isTouchWall || rb.velocity.y > 0)
        {
            isSlide = false;
        }
    }
}
