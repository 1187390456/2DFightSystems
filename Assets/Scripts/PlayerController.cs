using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb; // 自身刚体
    private Animator at; // 自身动画

    private float horizontalDirection; // 水平输入方向

    private bool isCanJump; // 是否能够跳跃
    private bool isTouchGround; // 是否触地
    private bool isTouchWall; // 是否触墙
    private bool isWalking; // 是否在移动
    private bool isFacingright = true; // 是否面向右
    private bool isSlidingWall; // 是否在滑墙

    private int facingDirection = 1; // 面向方向 右1
    private int currentJumpCount; // 当前跳跃次数

    [Header("移动速度")] public float moveSpeed = 10.0f;
    [Header("跳跃力度")] public float jumpForce = 16.0f;
    [Header("滑墙速度")] public float slidingSpeed = 1f;
    [Header("最大跳跃次数")] int jumpCountMax = 3; //最大跳跃次数

    [Header("检测层级")] public LayerMask checkLayer;
    [Header("地面检测点")] public Transform groundCheck;
    [Header("墙壁检测点")] public Transform wallCheck;
    [Header("地面检测盒子大小")] public Vector2 groundCheckBoxSize = new Vector2(0.58f, 0.02f);
    [Header("墙壁检测射线距离")] public float wallCheckDistance = 0.36f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        at = GetComponent<Animator>();
    }
    private void Start()
    {
        currentJumpCount = jumpCountMax;
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
            Jump();
        }
    }
    // 检测跳跃状态
    private void CheckJumpState()
    {
        if (isTouchGround && rb.velocity.y < 0.01f)
        {
            currentJumpCount = jumpCountMax;
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
        if (!isTouchGround && isTouchWall && rb.velocity.y < 0)
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
        isFacingright = !isFacingright;
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    // 移动
    private void Move()
    {
        rb.velocity = new Vector2(horizontalDirection * moveSpeed, rb.velocity.y);
        if (isSlidingWall)
        {
            if (Mathf.Abs(rb.velocity.y) > slidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -slidingSpeed);
            }
        }
    }
    // 跳跃
    private void Jump()
    {
        if (isCanJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            currentJumpCount--;
        }

    }
}
