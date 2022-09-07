using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("无限跳跃")]
    public bool infinityJump;

    Transform groundCheck; // 地面检查位置
    Transform wallCheck; // 墙壁检查位置
    Animator animator; // 自身动画
    Rigidbody2D rg; // 自身刚体

    [Header("水平移动速度")]
    public float moveSpeed = 10f; // 移动速度
    [Header("跳跃力度")]
    public float jumpForce = 16f; // 跳跃力度
    [Header("滑墙下降速度")]
    public float sildeSpeed = 2f; // 滑墙速度
    float groundCheckRadius = 0.3f; // 地面检查半径
    float wallCheckDistance = 0.4f; // 墙壁检查半径
    float horizontalInput; // 用户水平输入

    [Header("可以跳跃次数")]
    public int CanJumpCount = 3; // 可以跳跃次数
    int currentJumpCount; // 当前可以跳跃次数

    bool isFaceRight = true; // 是否面向右方
    bool isWalking; // 角色是否在移动
    bool isTouchGround; // 是否触地
    bool isTouchWall; // 是否触墙
    bool isCanJump; // 是否可以跳跃
    bool isCanSlideWall; // 是否可以滑墙

    [Header("地面层级")]
    public LayerMask groundLayer; // 地面层级

    private void Awake()
    {
        GetAllAttribute();
    }
    private void Start()
    {
        InitOperate();
    }
    private void Update()
    {
        CheckUserInput();
        CheckStatus();
        UpdateAnimations();
    }
    private void FixedUpdate()
    {
        CheckIsTouch();
        StartMove();
    }
    private void OnDrawGizmos()
    {
        // 以一个点 和半径 画圆
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        // 以一个点 和半径 画线
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
    // 初始获取自身属性
    private void GetAllAttribute()
    {
        rg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
        wallCheck = transform.Find("WallCheck");
    }
    // 初始操作
    private void InitOperate()
    {
        currentJumpCount = CanJumpCount;
    }
    // 检查用户输入
    private void CheckUserInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            StartJump();
        }
    }
    // 检测角色朝向
    private void CheckDirection()
    {
        if (isFaceRight && horizontalInput < 0)
        {
            TurnDirection();
        }
        else if (!isFaceRight && horizontalInput > 0)
        {
            TurnDirection();
        }
    }
    // 检测角色状态
    private void CheckStatus()
    {
        CheckDirection();
        CheckWalkStatus();
        ChekJumpStatus();
        CheckSlideStatus();
    }
    // 检查行走状态
    private void CheckWalkStatus()
    {
        if (horizontalInput != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
    // 检查跳跃状态
    private void ChekJumpStatus()
    {
        if (isTouchGround && rg.velocity.y <= 0)
        {
            currentJumpCount = CanJumpCount;
            isCanJump = true;
        }
        if (currentJumpCount <= 0)
        {
            isCanJump = false;
        }
        else
        {
            isCanJump = true;
        }
    }
    // 检测滑动状态
    private void CheckSlideStatus()
    {
        if (isTouchWall && rg.velocity.y <= 0 && !isTouchGround)
        {
            isCanSlideWall = true;
        }
        else
        {
            isCanSlideWall = false;
        }
    }
    // 检查接触
    private void CheckIsTouch()
    {
        // 检测碰到地面 检查碰撞体是否位于一个圆形区域内
        isTouchGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        // 检测碰到墙壁
        isTouchWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, groundLayer);
    }
    // 更新动画状态
    private void UpdateAnimations()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGround", isTouchGround);
        animator.SetFloat("yVelocity", rg.velocity.y);
        animator.SetBool("isSlideWall", isCanSlideWall);
    }
    // 角色转向
    private void TurnDirection()
    {
        if (!isCanSlideWall)
        {
            isFaceRight = !isFaceRight;
            transform.Rotate(0, 180, 0);
        }
    }
    // 开始移动
    private void StartMove()
    {
        //地面
        if (isTouchGround)
        {
            rg.velocity = new Vector2(horizontalInput * moveSpeed, rg.velocity.y);
        }
        // 空中
        else if (!isTouchGround && !isTouchWall && horizontalInput != 0)
        {
            rg.velocity = new Vector2(horizontalInput * moveSpeed, rg.velocity.y);


            //Vector2 forceAdd = new Vector2(horizontalInput * forceInTheAir, 0);
            //rg.AddForce(forceAdd);
            //// 如果水平力大于空气力 则使用水平的力
            //if (Mathf.Abs(rg.velocity.x) > forceInTheAir)
            //{
            //    rg.velocity = new Vector2(horizontalInput * moveSpeed, rg.velocity.y);
            //}
        }
        // 滑墙
        if (isCanSlideWall)
        {
            if (rg.velocity.y <= -sildeSpeed)
            {
                rg.velocity = new Vector2(rg.velocity.x, -sildeSpeed);
            }
        }
    }
    // 开始跳跃
    private void StartJump()
    {
        if (isCanJump || infinityJump)
        {
            rg.velocity = new Vector2(rg.velocity.x, jumpForce);
            currentJumpCount--;
        }
    }
}
