using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb; // 自身刚体
    private Animator at; // 自身动画

    private bool canJump = true; // 是否能够跳跃
    private bool canMove = true; // 是否能移动
    private bool canTurn = true; // 是否能够翻转
    private bool canClimb = false; // 是否能够爬角

    private bool isTouchGround; // 是否触地
    private bool isTouchWall; // 是否触墙
    private bool isTouchEdge; // 是否触碰边缘

    private bool isSlidingWall; // 是否在滑墙
    private bool isMoveing; // 是否在移动
    private bool isRuning = false; //是否在奔跑
    private bool isDashing; // 是否在冲刺

    private float horizontalDirection; // 水平输入方向
    private bool isFacingright = true; // 是否面向右
    private int facingDirection = 1; // 面向方向 右1
    private int currentJumpCount; // 当前跳跃次数

    private bool isReachEdge; // 是否到达边缘
    private Vector2 edgePoint; // 边缘点
    private Vector2 climbStartPos; //  爬墙动画起始点
    private Vector2 climbEndPos; //  爬墙动画终点

    private float lastDashTime; //最后一次冲刺时间点

    private float dashTimeLeft; // 剩余冲刺时长



    private float lastDashPosX; // 最后的残影X轴位置


    [Header("爬墙动画终点偏移")] public Vector2 climbEndOffset = new Vector2(0.5f, 2f);

    [Header("最大冲刺时长")] public float dashTimeMax;
    [Header("冲刺冷却")] public float dashCoolDown;
    [Header("冲刺速度")] public float dashSpeed;
    [Header("残影间距")] public float dashSpace;

    [Header("移动速度")] public float moveSpeed = 10.0f;
    [Header("跳跃力度")] public float jumpForce = 20.0f;
    [Header("滑墙速度")] public float slidingSpeed = 1f;
    [Header("最大跳跃次数")] int jumpCountMax = 3;
    [Header("空气阻力乘数")] public Vector2 airForceMultiplier = new Vector2(0.5f, 0.5f);

    [Header("检测层级")] public LayerMask checkLayer;
    [Header("地面检测点")] public Transform groundCheck;
    [Header("墙壁检测点")] public Transform wallCheck;
    [Header("边缘检测点")] public Transform edgeCheck;
    [Header("地面检测盒子大小")] public Vector2 groundCheckBoxSize = new Vector2(0.58f, 0.02f);
    [Header("墙壁检测射线距离")] public float wallCheckDistance = 0.35f;
    [Header("边缘检测射线距离")] public float edgeCheckDistance = 0.5f;

    [Header("跳墙力度")] public float wallJumpForce = 24.0f;
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
        CheckMoveState();
        CheckClimbState();
        CheckDashState();
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
        Gizmos.DrawLine(edgeCheck.position, new Vector2(edgeCheck.position.x + edgeCheckDistance, edgeCheck.position.y));
    }
    // 检测用户输入
    private void CheckUserInput()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        // 开启奔跑状态
        if (Input.GetKeyDown(KeyCode.H))
        {
            isRuning = !isRuning;
        }
        // 过了冷却时间开启冲刺状态 
        if (Input.GetKeyDown(KeyCode.L) && Time.time > lastDashTime + dashCoolDown)
        {
            Dash();
        }
        CheckJumpInput();
    }
    // 检测跳跃按键
    private void CheckJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isSlidingWall && Input.GetKey(KeyCode.S))
            {
                JumpFormWall();
            }
            else if (isTouchWall)
            {
                JumpInTheWall();
            }
            else
            {
                NormalJump();
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * airForceMultiplier.y);
        }
    }
    // 检测跳跃状态
    private void CheckJumpState()
    {
        if ((isTouchGround && rb.velocity.y <= 0.01f) || isSlidingWall)
        {
            currentJumpCount = jumpCountMax;
        }
        if (currentJumpCount <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }
    // 检测冲刺状态
    private void CheckDashState()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canTurn = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                // 当角色移动距离超过指定距离 从对象池中生成一个残影 即残影间距
                if (Mathf.Abs(transform.position.x - lastDashPosX) > dashSpace)
                {
                    ObjectPool.Instance.GetObjFormPool();
                    lastDashPosX = transform.position.x;
                }
            }
            if (dashTimeLeft <= 0)
            {
                canMove = true;
                canTurn = true;
                isDashing = false;
            }
        }
    }
    // 检测移动状态
    private void CheckMoveState()
    {
        if (horizontalDirection != 0)
        {
            isMoveing = true;
        }
        else
        {
            isMoveing = false;
        }
    }
    // 检测爬角状态
    private void CheckClimbState()
    {
        if (isReachEdge && !canClimb)
        {
            canClimb = true;
            if (isFacingright)
            {
                climbStartPos = new Vector2(Mathf.Floor(edgePoint.x) + wallCheckDistance * 2, Mathf.Floor(edgePoint.y));
                climbEndPos = new Vector2(Mathf.Floor(edgePoint.x) + climbEndOffset.x * 2 + wallCheckDistance, Mathf.Floor(edgePoint.y) + climbEndOffset.y);
            }
            else
            {
                climbStartPos = new Vector2(Mathf.Floor(edgePoint.x) + wallCheckDistance, Mathf.Floor(edgePoint.y));
                climbEndPos = new Vector2(Mathf.Floor(edgePoint.x) - climbEndOffset.x, Mathf.Floor(edgePoint.y) + climbEndOffset.y);
            }
            canMove = false;
            canTurn = false;
            at.SetBool("canClimb", canClimb);
        }
        if (canClimb)
        {
            transform.position = climbStartPos;
        }
    }
    // 爬墙动画结束回调
    public void ClimbAnimationDone()
    {
        canClimb = false;
        canMove = true;
        canTurn = true;
        isReachEdge = false;
        transform.position = climbEndPos;
        at.SetBool("canClimb", canClimb);

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
        isTouchEdge = Physics2D.Raycast(edgeCheck.position, transform.right, edgeCheckDistance, checkLayer);
        if (!isTouchEdge && isTouchWall && !isReachEdge)
        {
            isReachEdge = true;
            edgePoint = wallCheck.position;
        }

    }
    // 检测滑墙状态
    private void CheckSlidingWallState()
    {
        if (isTouchWall && rb.velocity.y < 0 && !canClimb)
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
        at.SetBool("isMoveing", isMoveing);
        at.SetBool("isSlideWall", isSlidingWall);
        at.SetBool("isRuning", isRuning);
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
        // 地面
        if (isTouchGround && canMove)
        {
            rb.velocity = new Vector2(horizontalDirection * moveSpeed, rb.velocity.y);
        }
        // 空中 控制
        else if (!isTouchGround && !isTouchWall && horizontalDirection != 0 && canMove)
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
    // 冲刺
    private void Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;
        dashTimeLeft = dashTimeMax;

        // 生成一个残影 记录当前上一次残影位置
        ObjectPool.Instance.GetObjFormPool();
        lastDashPosX = transform.position.x;
    }
    // 在墙上跳跃
    private void JumpInTheWall()
    {
        isSlidingWall = false;
        currentJumpCount--;
        Vector2 forceAdd = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
        rb.AddForce(forceAdd, ForceMode2D.Impulse);
        Turn();
    }
    // 墙上跳下
    private void JumpFormWall()
    {
        isSlidingWall = false;
        currentJumpCount--;
        Vector2 forceAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
        rb.AddForce(forceAdd, ForceMode2D.Impulse);
    }
    // 正常跳跃
    private void NormalJump()
    {
        if (canJump && !isSlidingWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            currentJumpCount--;
        }
    }
}
