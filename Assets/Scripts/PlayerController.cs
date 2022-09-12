using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb; // 自身刚体
    private Animator at; // 自身动画

    private float horizontalDirection; // 水平输入方向

    private bool isTouchGround; // 是否触地
    private bool isTouchWall; // 是否触墙
    private bool isWalking; // 是否在移动
    private bool isFacingright = true; // 是否面向右
    private bool isSlidingWall; // 是否在滑墙
    private bool canNormalJump; // 是否能够正常跳跃
    private bool canWallJump; // 是否可以墙跳
    private bool canMove; // 是否能够移动 
    private bool canTurn; // 是否能够转身
    private bool isWantToJump; // 是否想要进行非正常跳跃
    private bool checkJumpMultiplier; // 检查跳转乘数 控制高度
    private bool hasWallJump; // 是否在墙跳

    private float jumpTimer;// 非正常跳跃计时器
    private float jumpTimerSet = 0.15f;// 非正常跳跃计时器设置

    private float turnTimer;// 墙壁跳跃转身计时器
    private float turnTimerSet = 0.1f;// 墙壁跳跃转身计时器设置

    private float wallJumpTimer; // 墙跳计时器
    private float wallJumpTimerSet = 0.5f; // 墙跳计时器设置

    private int facingDirection = 1; // 面向方向 右1
    private int currentJumpCount; // 当前跳跃次数
    private int wallJumpLastDirection; // 墙跳方向

    [Header("移动速度")] public float moveSpeed = 10.0f;
    [Header("跳跃力度")] public float jumpForce = 16.0f;
    [Header("滑墙速度")] public float slidingSpeed = 1f;
    [Header("最大跳跃次数")] int jumpCountMax = 3;
    [Header("水平空气阻力")] public float airForceX = 10f;
    [Header("空气阻力乘数")] public Vector2 airForceMultiplier = new Vector2(0.5f, 0.5f);

    [Header("检测层级")] public LayerMask checkLayer;
    [Header("地面检测点")] public Transform groundCheck;
    [Header("墙壁检测点")] public Transform wallCheck;
    [Header("地面检测盒子大小")] public Vector2 groundCheckBoxSize = new Vector2(0.58f, 0.02f);
    [Header("墙壁检测射线距离")] public float wallCheckDistance = 0.36f;

    [Header("跳墙力度")] public float wallJumpForce = 30.0f;
    [Header("瞪墙力度")] public float wallHopForce = 3.0f;
    [Header("跳墙方向")] public Vector2 wallJumpDirection = new Vector2(1.0f, 2.0f);
    [Header("瞪墙方向")] public Vector2 wallHopDirection = new Vector2(1.0f, 0.5f);


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        at = GetComponent<Animator>();
    }
    private void Start()
    {
        currentJumpCount = jumpCountMax;
        wallJumpDirection.Normalize();
        wallHopDirection.Normalize();
    }
    private void Update()
    {
        CheckUserInput();
        CheckJumpState();
        CheckPlayerDirection();
        CheckMovestate();
        CheckSlidingWallState();
        UpdateAnimation();
    }
    private void FixedUpdate()
    {
        Move();
        CheckEnvironment();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position, groundCheckBoxSize);
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    // 检测用户输入
    private void CheckUserInput()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            // 地面或者墙壁上可以跳
            if (isTouchGround || (isTouchWall && currentJumpCount > 0))
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isWantToJump = true;
            }
        }
        if (Input.GetButtonDown("Horizontal") && isTouchWall)
        {
            if (!isTouchGround && horizontalDirection != facingDirection)
            {
                canMove = false;
                canTurn = false;
                turnTimer = turnTimerSet;
            }
        }
        if (!canMove)
        {
            turnTimer -= Time.deltaTime;
            if (turnTimer < 0)
            {
                canMove = true;
                canTurn = true;
            }
        }

        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * airForceMultiplier.y);
        }
    }
    // 检测跳跃状态
    private void CheckJumpState()
    {
        if ((isTouchGround && rb.velocity.y < 0.01f))
        {
            currentJumpCount = jumpCountMax;
        }
        if (isTouchWall)
        {
            canWallJump = true;
        }
        if (currentJumpCount <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
        // 想要进行非正常跳跃

        if (jumpTimer > 0)
        {
            // 墙跳
            if (!isTouchGround && isTouchWall && horizontalDirection != 0 && horizontalDirection != facingDirection)
            {
                WallJump();
            }
            else if (isTouchGround)
            {
                NormalJump();
            }
        }
        if (isWantToJump)
        {
            jumpTimer -= Time.deltaTime;
        }
        if (wallJumpTimer > 0)
        {
            if (hasWallJump && horizontalDirection == -wallJumpLastDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                hasWallJump = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJump = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
    }
    // 检测移动状态
    private void CheckMovestate()
    {
        if (horizontalDirection != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
    // 检测玩家方向
    private void CheckPlayerDirection()
    {
        if (isFacingright && horizontalDirection < 0)
        {
            Turn();
        }
        else if (!isFacingright && horizontalDirection > 0)
        {
            Turn();
        }
    }
    // 检测周围环境
    private void CheckEnvironment()
    {
        isTouchGround = Physics2D.OverlapBox(groundCheck.position, groundCheckBoxSize, 0.0f, checkLayer);
        isTouchWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, checkLayer);
    }
    // 检测滑墙状态
    private void CheckSlidingWallState()
    {
        if (isTouchWall && horizontalDirection == facingDirection && rb.velocity.y < 0)
        {
            isSlidingWall = true;
        }
        else
        {
            isSlidingWall = false;
        }
    }
    // 更新动画
    private void UpdateAnimation()
    {
        at.SetFloat("yVelocity", rb.velocity.y);
        at.SetBool("isGround", isTouchGround);
        at.SetBool("isWalking", isWalking);
        at.SetBool("isSlideWall", isSlidingWall);
    }
    // 转身
    private void Turn()
    {
        if (!isSlidingWall && canTurn)
        {
            isFacingright = !isFacingright;
            facingDirection *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
    // 移动
    private void Move()
    {
        if (!isTouchGround && !isSlidingWall && horizontalDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airForceMultiplier.x, rb.velocity.y);
        }
        else if (canMove)
        {
            rb.velocity = new Vector2(horizontalDirection * moveSpeed, rb.velocity.y);
        }

        if (isSlidingWall)
        {
            if (Mathf.Abs(rb.velocity.y) > slidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -slidingSpeed);
            }
        }
    }

    // 墙跳
    private void WallJump()
    {
        if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            isSlidingWall = false;
            currentJumpCount = jumpCountMax;
            currentJumpCount--;
            Vector2 forceAdd = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isWantToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canTurn = true;
            canMove = true;
            hasWallJump = true;
            wallJumpTimer = wallJumpTimerSet;
            wallJumpLastDirection = -facingDirection;
        }
    }
    // 正常跳跃
    private void NormalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            currentJumpCount--;
            jumpTimer = 0;
            isWantToJump = false;
            checkJumpMultiplier = true;
        }
    }
}
