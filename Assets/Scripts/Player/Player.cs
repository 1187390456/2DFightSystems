using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Player : MonoBehaviour
{
    float movementInputDirection;//移动输入方向
    float jumpTimer;//跳跃计时器
    float turnTimer;//转动计时器
    float wallJumpTimer;//墙面跳跃计时器
    float dashTimeLeft;//剩余冲刺时间
    float lastImageXpos;//最后图片位置
    float lastDash = -100f;//最后冲刺
    float knockbackStartTime;//击退开始时间
    [SerializeField]
    float knockbackDuration;//击退持续时间

    Rigidbody2D rb;//刚体
    Animator anim;//动画
    Vector2 ledgePosBot;//窗台位置底部
    Vector2 ledgePos1;//窗台位置
    Vector2 ledgePos2;//窗台位置

    int amountOfJumpsLeft;//剩余跳跃次数
    int facingDirection = 1;//面向方向
    int lastWallJumpDirection;//最后墙面跳跃方向

    bool isFacing = true;//面向右为true、左为false
    bool isWalking;//行走
    bool isGrounded;//接触地面
    bool isTouchingWall;//接触墙
    bool isWallSliding;//墙壁滑动
    bool canNormalJump;//能否正常跳跃
    bool canWallJump;//能否贴墙跳跃
    bool isAttemptingToJump;//试图跳跃
    bool checkJumpMultiplier;//检查跳转乘数
    bool canMove;//能否移动
    bool canFlip;//能否翻转
    bool hasWallJumped;//有跳墙
    bool isTouchingLedge;//接触窗台
    bool canClimbLedge = false;//能否攀爬窗台
    bool ledgeDetected;//窗台检测
    bool isDashing;//冲刺
    bool knockback;//击退

    public float jumpForce = 16.0f;//跳跃力度
    public float movementSpeed = 10.0f;//移动速度
    public float groundCheckRadius;//地面检查半径
    public float wallCheckDistance;//墙面的距离
    public float wallSlideSpeed;//墙面滑动速度
    public float movementForceInAir;//空气阻力
    public float airDragMultiplier = 0.95f;//空气阻力系数
    public float variableJumpHeightMultiplier = 0.5f;//变量跳跃高度乘数
    public float wallHopForce;//跳跃力
    public float wallJumpForce;//跳跃力
    public float jumpTimerSet = 0.15f;//跳跃计时器设置
    public float turnTimerSet = 0.1f;//转动计时器设置
    public float wallJumpTimerSet = 0.5f;//墙面跳跃计时器设置
    public float ledgeClimbXOffset1 = 0f;// 窗台攀爬
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;
    public float dashTime;//冲刺时间
    public float dashSpeed;//冲刺速度
    public float distanceBetweenImages;//图像之间的距离
    public float dashCoolDown;//冲刺冷却

    [SerializeField]
    Vector2 knockbackSpeed;//击退速度 

    public Vector2 wallHopDirection;//跳墙方向
    public Vector2 wallJumpDirection;//跳墙方向
    public Transform groundCheck;//地面
    public Transform wallCheck;//墙面
    public Transform ledgeCheck;//窗台
    public LayerMask whatIsGround;//地面图层

    public int amountOfJumps = 1;//跳跃次数

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();//返回大小为1的向量(只读)。规范化时，向量保持相同的方向，但其长度为1.0。如果向量太小而不能被归一化，则返回一个零向量
        wallJumpDirection.Normalize();
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
        CheckLedgeClimb();
        CheckDash();
        CheckKnockback();
    }
    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }
    //检查输入（按什么按钮）
    void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");//   A/<-返回-1，D/->返回1

        if (Input.GetButtonDown("Jump"))//按下空格
        {
            if (isGrounded || (amountOfJumpsLeft > 0 && isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if (!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }

        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        if (Input.GetKey(KeyCode.L))
        {
            if (Time.time >= (lastDash + dashCoolDown))
                AttemptToDash();
        }
    }
    //移动
    void ApplyMovement()
    {
        if (!isGrounded && !isWallSliding && movementInputDirection == 0 && !knockback)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if (canMove && !knockback)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y); // 刚体速度设置，x轴
        }

        if (isWallSliding)//滑动
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }
    //检查移动方向
    void CheckMovementDirection()
    {
        if (isFacing && movementInputDirection < 0)//面像右边并且按下的是A或<-
        {
            Flip();//转向
        }
        else if (!isFacing && movementInputDirection > 0)//面像左边并且按下的是D或->
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) >= 0.01f)//横向移动速度不为0
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
    //转向
    void Flip()
    {
        if (!isWallSliding && canFlip && !knockback)
        {
            facingDirection *= -1;//反方向为-1
            isFacing = !isFacing;
            transform.Rotate(0.0f, 180.0f, 0.0f);//Y轴旋转180
        }
    }
    //关闭转向
    void DisableFlip()
    {
        canFlip = false;
    }
    //启动转向
    void EnableFlip()
    {
        canFlip = true;
    }
    //检查跳跃
    void CheckJump()
    {
        if (jumpTimer > 0)
        {
            //墙壁跳跃
            if (!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
            {
                WallJump();
            }
            else if (isGrounded)
            {
                NormalJump();
            }
        }

        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }

        if (wallJumpTimer > 0)
        {
            if (hasWallJumped && movementInputDirection == -lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                hasWallJumped = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
    }
    //更新动画
    void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }
    //检查环境（地面，墙壁）
    void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //射线
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;
        }
    }
    //检查是否能跳跃
    void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)//接地 && 速度小于等于0
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (isTouchingWall)
        {
            canWallJump = true;
        }

        if (amountOfJumpsLeft <= 0)//跳跃次数没有了，禁止跳跃
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
    }
    //检查墙壁是否滑动
    void CheckIfWallSliding()
    {
        if (isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0 && !canClimbLedge)// 接触墙面 && 不接触地面 && y轴方向小于0
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }
    //正常的跳
    void NormalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);// 刚体速度设置，y轴
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }
    }
    //墙跳
    void WallJump()
    {
        if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;
        }
    }
    //检查窗台攀爬
    void CheckLedgeClimb()
    {
        if (ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if (isFacing)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }

            canMove = false;
            canFlip = false;

            anim.SetBool("canClimbLedge", canClimbLedge);
        }

        if (canClimbLedge)
        {
            transform.position = ledgePos1;
        }
    }
    //完成窗台攀爬
    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        ledgeDetected = false;
        anim.SetBool("canClimbLedge", canClimbLedge);
    }
    //打算冲刺
    void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }
    //检查冲刺
    void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, 0);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0 || isTouchingWall)
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
            }
        }
    }
    //面临方向
    public int GetFacingDirection()
    {
        return facingDirection;
    }
    //击退
    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }
    //检查击退
    void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }
    //得到缓冲状态
    public bool GetDashStatus()
    {
        return isDashing;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y + wallCheck.position.z));
    }
}
