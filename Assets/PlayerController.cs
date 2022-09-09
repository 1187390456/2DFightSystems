using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    private Animator at; // 自身动画
    private Rigidbody2D rb; // 自身刚体

    private float horizontalDirection; // 玩家输入方向

    private bool isFacingRight = true; // 是否面向右边
    private bool isTouchGround; // 是否触地
    private bool isTouchWall; // 是否触墙
    private bool isSildingWall; // 是否正在墙上滑行
    private bool isCanJump;// 是否可以跳跃

    private int currentJumpCount; //当前跳跃次数

    [Header("地面检测点")] public Transform groundCheck;
    [Header("墙壁检测点")] public Transform wallCheck;
    [Header("地面检测盒子大小")] public Vector2 groundCheckBoxSize = new Vector2(0.58f, 0.01f);
    [Header("墙壁检测距离")] public float wallCheckDistance = 0.5f;
    [Header("检测层级")] public LayerMask checkLayer;

    [Header("移动速度")] public float moveSpeed = 10.0f;
    [Header("跳跃力度")] public float jumpForce = 16.0f;
    [Header("可跳跃次数")] public int jumpCount = 3;
    [Header("墙壁滑行速度")] public float slideWallSpeed = 1.0f;
    [Header("空气阻力")] public Vector2 airForce = new Vector2(0.95f, 0.5f);


    bool isWalking
    {
        get { return horizontalDirection != 0; }
    } // 角色是否在移动

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        at = GetComponent<Animator>();
    }
    private void Start()
    {
        currentJumpCount = jumpCount;
    }
    private void Update()
    {
        CheckInput();
        CheckDirection();
        CheckWallSilde();
        CheckJumpStatus();
        UpdateAnimation();
    }
    private void FixedUpdate()
    {
        StartMove();
        CheckEnvironment();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireCube(groundCheck.position, groundCheckBoxSize);
    }
    // 检查用户输入
    private void CheckInput()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    // 检查角色方向
    private void CheckDirection()
    {
        if (isFacingRight && horizontalDirection < 0)
        {
            TurnAround();
        }
        else if (!isFacingRight && horizontalDirection > 0)
        {
            TurnAround();
        }
    }

    // 检测是否在墙壁滑行
    private void CheckWallSilde()
    {
        if (isTouchWall && !isTouchGround && rb.velocity.y < 0)
        {
            isSildingWall = true;
        }
        else
        {
            isSildingWall = false;
        }
    }
    // 检测跳跃状态
    private void CheckJumpStatus()
    {
        if (isTouchGround || isTouchWall && rb.velocity.y < 0.01f)
        {
            currentJumpCount = jumpCount;
        }
        if (currentJumpCount > 0)
        {
            isCanJump = true;
        }
        else
        {
            isCanJump = false;
        }
    }
    // 更新动画
    private void UpdateAnimation()
    {
        at.SetBool("isWalking", isWalking);
        at.SetFloat("yVelocity", rb.velocity.y);
        at.SetBool("isGround", isTouchGround);
        at.SetBool("isSlideWall", isSildingWall);
    }
    // 跳跃
    private void Jump()
    {
        if (isCanJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Invoke("LayzeCount", 0.04f);
        }
    }
    // 跳跃次数延时
    private void LayzeCount()
    {
        currentJumpCount--;
    }
    // 角色转向
    private void TurnAround()
    {
        if (!isSildingWall)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180f, 0.0f);
        }
    }
    // 检查环境
    private void CheckEnvironment()
    {
        isTouchGround = Physics2D.OverlapBox(groundCheck.position, groundCheckBoxSize, 0, checkLayer);
        isTouchWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, checkLayer);
    }
    // 开始移动
    private void StartMove()
    {
        if (!isTouchGround && !isTouchWall && horizontalDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airForce.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontalDirection * moveSpeed, rb.velocity.y);
        }
        if (isSildingWall)
        {
            if (rb.velocity.y < -slideWallSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -slideWallSpeed);
            }
        }
    }


}
