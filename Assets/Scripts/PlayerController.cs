#define ApplyNewMove
#define ApplyNewJump 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("无限跳跃")] public bool infinityJump;
    [Header("水平移动速度")] public float moveSpeed = 9f;
    [Header("跳跃力度")] public float jumpForce = 20f;
    [Header("滑墙下降速度")] public float sildeSpeed = 1f;
    [Header("角色空中尝试移动的施加力大小")] public float playerMoveInTheAir = 50f;
    [Header("水平空气阻力")] public float airForceX = 0.95f;
    [Header("垂直空气阻力")] public float airForceY = 0.5f;
    [Header("角色在墙上跳跃时的力度")] public float wallJumpForce = 30f;
    [Header("角色在墙上挂着时的力度")] public float wallHangForce = 10f;
    [Header("大跳持续时间")] public float jumpTimerSet = 0.15f;
    [Header("爬墙转身持续时间")] public float turnTimerSet = 0.1f;
    [Header("地面层级")] public LayerMask groundLayer;
    [Header("角色在墙上跳跃时的方向")] public Vector2 wallHangDirection = new Vector2(1, 2);
    [Header("角色在墙上挂着时的方向")] public Vector2 wallJumpDirection = new Vector2(1, 0.5f);
    [Header("可以跳跃次数")] public int canJumpCount = 3;

    Animator animator; // 自身动画
    Rigidbody2D rg; // 自身刚体

    float horizontalInput
    {
        get { return Input.GetAxisRaw("Horizontal"); }
    }// 用户水平输入
    float jumpTimer; // 大跳计时器
    float turnTimer; // 爬墙转身计时器

    int currentJumpCount; // 当前可以跳跃次数
    int faceWallDirection = 1; // 1在右边 -1在左边

    bool isFaceRight = true; // 是否面向右方
    bool isWantToJumpInTheWall; // 是否在墙上试图去跳跃
    bool isCanNormalJump; // 是否可以正常跳跃
    bool isCanWallJump; // 是否可以在墙上跳跃
    bool isCanHandleJumpHeight; // 是否可控跳跃高度
    bool isCanMove; // 是否可以移动
    bool isCanTurn; // 是否可以翻转 
    bool IsWalking
    {
        get { return horizontalInput != 0; }
    }// 角色是否在移动 
    bool IsSlidingWall
    {
        get { return IsTouchWall && horizontalInput == faceWallDirection && rg.velocity.y < 0; }
    } // 角色是否在滑墙

    [Obsolete("废弃但是保留")]
    bool isCanJump = false;

    /// <summary>
    /// 检查是否触地
    /// </summary>
    #region
    Transform wallCheck;
    [Header("地面检查半径")] public float groundCheckRadius = 0.3f;
    public bool IsTouchGround
    {
        // 检查碰撞体是否位于一个圆形区域内
        get { return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); }
    }
    #endregion

    /// <summary>
    /// 检查是否触墙
    /// </summary>
    #region
    Transform groundCheck;
    [Header("墙壁检查半径")] public float wallCheckDistance = 0.65f;
    public bool IsTouchWall
    {
        // 检查碰撞体被发射的射线触到
        get { return Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, groundLayer); }
    }
    #endregion

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
        StartMove();
        StartJump();
    }
    private void OnDrawGizmos()
    {
        // 以一个点 和半径 画圆
        //  Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        // 以一个点 和半径 画线
        //   Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
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
        currentJumpCount = canJumpCount;
        // 方向向量归一 表示方向
        wallHangDirection.Normalize();
        wallJumpDirection.Normalize();
    }
    // 检查用户输入
    private void CheckUserInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            // 触地 或 触墙可以跳时 正常跳
            if (IsTouchGround || (IsTouchWall && currentJumpCount > 0))
            {
                NormalJump();
            }
            else
            {
                // 重置定时器 可以进行墙跳
                jumpTimer = jumpTimerSet;
                isWantToJumpInTheWall = true;
            }

        }
        // 墙壁上输入
        if (Input.GetButtonDown("Horizontal") && IsTouchWall)
        {
            // 输入与面向方向相反 且不触地
            if (!IsTouchGround && horizontalInput != faceWallDirection)
            {
                isCanMove = false;
                isCanTurn = false;
                turnTimer = turnTimerSet; // 重置转向定时器
            }
        }
        // 不能移动时计时 这段时间假装被粘在墙上
        if (!isCanMove)
        {
            turnTimer -= Time.deltaTime;
            if (turnTimer <= 0)
            {
                isCanMove = true;
                isCanTurn = true;
            }
        }
        // 可变高度 且离开空格按钮
        if (isCanHandleJumpHeight && !Input.GetButton("Jump"))
        {
            isCanHandleJumpHeight = false;
            rg.velocity = new Vector2(rg.velocity.x, rg.velocity.y * airForceY);
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
        ChekJumpStatus();
    }
    // 检查跳跃状态
    private void ChekJumpStatus()
    {
        if (IsTouchGround && rg.velocity.y <= 0.01f)
        {
            currentJumpCount = canJumpCount;
        }
        if (IsTouchWall)
        {
            isCanWallJump = true;
        }

        if (currentJumpCount <= 0)
        {
            isCanNormalJump = false;
        }
        else
        {
            isCanNormalJump = true;
        }
    }
    // 更新动画状态
    private void UpdateAnimations()
    {
        animator.SetBool("isWalking", IsWalking);
        animator.SetBool("isGround", IsTouchGround);
        animator.SetFloat("yVelocity", rg.velocity.y);
        animator.SetBool("isSlideWall", IsSlidingWall);
    }
    // 角色转向
    private void TurnDirection()
    {
        if (!IsSlidingWall && isCanTurn)
        {
            faceWallDirection *= -1;
            isFaceRight = !isFaceRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
    // 开始移动
    private void StartMove()
    {
        OldMoveWay();
        NewMoveWay();
        // 滑墙
        if (IsSlidingWall)
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
        OldJumpWay();
        NewJumpWay();
    }
    // 正常跳
    private void NormalJump()
    {
        if (isCanNormalJump)
        {
            rg.velocity = new Vector2(rg.velocity.x, jumpForce);
            currentJumpCount--;
            jumpTimer = 0;
            isWantToJumpInTheWall = false;
            isCanHandleJumpHeight = true;
        }
    }
    // 跳墙
    private void WallJump()
    {
        if (isCanWallJump)
        {
            rg.velocity = new Vector2(rg.velocity.x, 0.0f); // 跳墙时去除重力
            currentJumpCount = canJumpCount;
            currentJumpCount--;
            Vector2 forceAdd = new Vector2(wallHangDirection.x * wallJumpForce * horizontalInput, wallJumpForce * wallHangDirection.y);
            rg.AddForce(forceAdd);
            jumpTimer = 0;
            isWantToJumpInTheWall = false;
            isCanHandleJumpHeight = true;
            turnTimer = 0;
            isCanTurn = true;
            isCanMove = true;
        }
    }
    [Conditional("ApplyNewMove")]
    // 新版移动
    private void NewMoveWay()
    {
        // 空中脱离控制
        if (!IsSlidingWall && !IsTouchGround && horizontalInput == 0)
        {
            rg.velocity = new Vector2(rg.velocity.x * airForceX, rg.velocity.y);
        }
        else if (isCanMove)
        {
            rg.velocity = new Vector2(horizontalInput * moveSpeed, rg.velocity.y);
        }
    }
    [Conditional("ApplyNewJump")]
    // 新版跳跃
    private void NewJumpWay()
    {
        if (jumpTimer > 0)
        {
            // 不触地 触墙 且控制 且控制变量与面向墙壁方向相反
            if (!IsTouchGround && IsTouchWall && horizontalInput != 0 && horizontalInput != faceWallDirection)
            {
                WallJump();
            }
            else if (IsTouchGround)
            {
                NormalJump();
            }
        }
        if (isWantToJumpInTheWall)
        {
            jumpTimer -= Time.deltaTime;
        }
    }
    // 旧版移动
    [Conditional("ApplyOldMove")]
    private void OldMoveWay()
    {
        //地面
        if (IsTouchGround)
        {
            rg.velocity = new Vector2(horizontalInput * moveSpeed, rg.velocity.y);
        }
        // 空中尝试移动
        else if (!IsTouchGround && !IsTouchWall && horizontalInput != 0)
        {
            Vector2 forceInAir = new Vector2(horizontalInput * playerMoveInTheAir, 0);
            rg.AddForce(forceInAir);
            // 如果速度过大 则 回归正常速度
            if (Mathf.Abs(rg.velocity.x) > moveSpeed)
            {
                rg.velocity = new Vector2(horizontalInput * moveSpeed, rg.velocity.y);
            }
        }
        // 空中失去控制
        else if (!IsTouchGround && !IsTouchWall && horizontalInput == 0)
        {
            rg.velocity = new Vector2(rg.velocity.x * airForceX, rg.velocity.y);
        }
    }
    // 旧版跳跃
    [Conditional("ApplyOldJump")]
    private void OldJumpWay()
    {
        if ((isCanJump && !IsSlidingWall) || infinityJump)
        {
            rg.velocity = new Vector2(rg.velocity.x, jumpForce);
            currentJumpCount--;
        }
        // 滑墙 或贴着墙 角色在控制时跳跃
        else if ((IsSlidingWall || IsTouchWall) && horizontalInput != 0 && isCanJump)
        {
            currentJumpCount--;
            Vector2 forceAdd = new Vector2(wallHangDirection.x * wallJumpForce * horizontalInput, wallJumpForce * wallHangDirection.y);
            rg.AddForce(forceAdd, ForceMode2D.Impulse);
        }
        // 滑墙  角色未控制时跳跃 
        else if (IsSlidingWall && horizontalInput == 0 && isCanJump)
        {
            currentJumpCount--;
            Vector2 forceAdd = new Vector2(wallJumpDirection.x * wallHangForce * -faceWallDirection, wallJumpDirection.y * wallHangForce);
            rg.AddForce(forceAdd, ForceMode2D.Impulse);
        }
    }
}
