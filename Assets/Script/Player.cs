using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform groundCheck;//地面检查
    public Transform wallCheck;//墙壁检查
    public Transform headerWallCheck;//头部墙壁检查
    public Vector2 startClimbAnimOffset = new Vector2(0.0f, -0.66f);//开始攀爬偏移量
    public Vector2 endClimbAnimOffsetRight = new Vector2(0.8f, 1.0f);//右边结束攀爬偏移量
    public Vector2 endClimbAnimOffsetLeft = new Vector2(-0.8f, 1.0f);//左边结束攀爬偏移量
    public float distance = 0.37f;//射线距离

    [Header("玩家移动速度")] public Vector2 playerSpeedV2 = new Vector2(10.0f, 17.0f);
    [Header("滑墙速度")] public float slideSpeed = -3f;
    [Header("跳跃次数")] public int jumpAmount;

    private Vector2 startClimbAnimPos;//开始攀爬动画位置
    private Vector2 endClimbAnimPos;//结束攀爬动画位置
    private Vector2 edgePos;//边界点位置
    private Vector2 slideV2;//滑墙方向
    private Rigidbody2D rb;//刚体
    private Animator anim;//动画

    private bool isCanMove = true;//能否移动
    private bool isMoving;//是否移动中
    private bool isFacing = true;//true右，false左
    private bool isCanJump;//能否跳跃
    private bool isSlide;//是否在滑墙
    private bool isTouchGround = false;//接触地面
    private bool isTouchWall;//接触墙壁
    private bool isTouchHeaderWall;//头部接触墙壁
    private bool isReachEdge;//是否到达偏移点
    private bool canClimb;//能否攀爬

    private float inputDirection;//输入方向
    private float groundCheckRadius = 0.23f;//地面检查圆形射线半径

    private int nowJumpAmount;//当前跳跃次数
    private int facingInt//面向右为1，左为-1
    {
        get
        {
            if (isFacing) return 1;
            return -1;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        slideV2 = new Vector2(1, 0);
    }
    private void Start()
    {
        nowJumpAmount = jumpAmount;
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
        CheckClimb();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + distance, wallCheck.position.y));
        Gizmos.DrawLine(headerWallCheck.position, new Vector2(headerWallCheck.position.x + distance, headerWallCheck.position.y));
    }
    //检查输入
    private void CheckInput()
    {
        inputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    //检查动画
    private void CheckAnimation()
    {
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isTouchGround", isTouchGround);
        anim.SetFloat("rbV", rb.velocity.y);
        anim.SetBool("isSlide", isSlide);
    }
    //检查环境
    private void CheckEnvironment()
    {
        slideV2 = new Vector2(facingInt, 0);
        isTouchGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("Ground"));
        isTouchWall = Physics2D.Raycast(wallCheck.position, slideV2, distance, LayerMask.GetMask("Ground"));
        isTouchHeaderWall = Physics2D.Raycast(headerWallCheck.position, slideV2, distance, LayerMask.GetMask("Ground"));
        if (isTouchWall && !isTouchHeaderWall && !isReachEdge)
        {
            isReachEdge = true;
            edgePos = wallCheck.position;
        }
    }
    //检查移动
    private void CheckMove()
    {
        if (inputDirection != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
    //移动
    private void Move()
    {
        if (isCanMove)
        {
            rb.velocity = new Vector2(inputDirection * playerSpeedV2.x, rb.velocity.y);
        }
    }
    //检查转向
    private void CheckFlip()
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
    private void Flip()
    {
        transform.localScale = new Vector3(-facingInt, 1, 1);
        isFacing = !isFacing;
    }
    //检查跳跃
    private void CheckJump()
    {
        if (isTouchGround || isTouchWall)
        {
            nowJumpAmount = jumpAmount;
        }
        if (nowJumpAmount > 0)
        {
            isCanJump = true;
        }
        else
        {
            isCanJump = false;
        }
    }
    //跳跃
    private void Jump()
    {
        if (isCanJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, playerSpeedV2.y);
            Invoke("DecreateCount", 0.02f);
        }
    }
    //减少跳跃次数
    private void DecreateCount()
    {
        nowJumpAmount--;
    }
    //检查滑墙
    private void CheckSlideWall()
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
    //检查攀爬
    private void CheckClimb()
    {
        if (isReachEdge && !canClimb)
        {
            canClimb = true;
            startClimbAnimPos = edgePos + startClimbAnimOffset;
            if (isFacing)
            {
                endClimbAnimPos = edgePos + endClimbAnimOffsetRight;
            }
            else
            {
                endClimbAnimPos = edgePos + endClimbAnimOffsetLeft;
            }
        }
        if (canClimb)
        {
            transform.position = startClimbAnimPos;
            anim.SetBool("canClimb", canClimb);
        }
    }
    //攀爬动画完成
    private void ClimbAnimDone()
    {
        isReachEdge = false;
        canClimb = false;
        anim.SetBool("canClimb", canClimb);
        transform.position = endClimbAnimPos;
    }
    //检查冲刺状态
    private void CheckDashState()
    {

    }
}
