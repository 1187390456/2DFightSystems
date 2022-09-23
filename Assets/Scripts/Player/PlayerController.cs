using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* 其他 */
    #region
    [Header("检测层级")] public LayerMask checkLayer;
    #endregion

    /* 公共属性 */
    #region
    public static PlayerController Instance; //单例
    public bool isFacingright = true; // 是否面向右
    public int facingDirection = 1; // 面向方向 1右
    public float horizontalDirection; // 水平输入
    #endregion

    /* 自身属性 */
    #region
    private Rigidbody2D rb; // 刚体
    private Animator at; // 动画
    #endregion

    /* 爬角 */
    #region
    [Header("爬墙动画终点偏移")] public Vector2 climbEndOffset = new Vector2(0.5f, 2f);
    [Header("边缘检测点")] public Transform edgeCheck;
    [Header("边缘检测射线距离")] public float edgeCheckDistance = 0.5f;
    private Vector2 climbEndPos; // 开始爬角复位
    private Vector2 climbStartPos; // 结束爬角复位
    private Vector2 edgePoint; // 边缘点
    private bool isReachEdge; // 是否达到边缘
    private bool isTouchEdge; // 头部检测
    private bool canClimb = false; // 是否能够爬角
    #endregion

    /* 冲刺 */
    #region
    [Header("冲刺冷却")] public float dashCoolDown = 0.5f;
    [Header("冲刺速度")] public float dashSpeed = 20.0f;
    [Header("最大冲刺时长")] public float dashTimeMax = 0.3f;
    [Header("残影间距")] public float dashSpace = 0.5f;
    private float dashTimeLeft; // 剩余冲刺时长
    private float lastDashPosX; // 上一次冲刺X坐标偏移
    private float lastDashTime; // 上一次冲刺时间
    private bool isDashing; // 是否在冲刺
    #endregion

    /* 跳跃 */
    #region
    [Header("地面检测点")] public Transform groundCheck;
    [Header("地面检测盒子大小")] public Vector2 groundCheckBoxSize = new Vector2(0.58f, 0.02f);
    [Header("跳跃力度")] public float jumpForce = 20.0f;
    [Header("最大跳跃次数")] private int jumpCountMax = 3;
    private bool canJump = true; // 是否能够跳跃
    private bool isTouchGround; // 是否触地
    private int currentJumpCount; // 当前跳跃次数
    #endregion

    /* 跳墙 */
    #region
    [Header("瞪墙方向")] public Vector2 wallHopDirection = new Vector2(1.0f, 0.5f);
    [Header("瞪墙力度")] public float wallHopForce = 3.0f;
    [Header("跳墙方向")] public Vector2 wallJumpDirection = new Vector2(1.0f, 2.0f);
    [Header("跳墙力度")] public float wallJumpForce = 24.0f;
    private bool isWallJumping; // 是否在墙上跳跃
    #endregion

    /* 移动 */
    #region
    [Header("移动速度")] public float moveSpeed = 10.0f;
    [Header("空气阻力乘数")] public Vector2 airForceMultiplier = new Vector2(0.5f, 0.5f);
    private bool canMove = true; // 能否移动
    private bool isMoveing; // 是否移动中
    private bool isRuning = false; // 是否奔跑中
    #endregion

    /* 转身 */
    #region
    private bool canTurn = true; // 能否转身
    #endregion

    /* 滑墙 */
    #region
    [Header("墙壁检测点")] public Transform wallCheck;
    [Header("墙壁检测射线距离")] public float wallCheckDistance = 0.35f;
    [Header("滑墙速度")] public float slidingSpeed = 1f;
    private bool isSlidingWall; // 是否在滑墙
    private bool isTouchWall; // 是否触墙
    #endregion

    /* 击退 */
    #region
    [Header("击退速度")] public Vector2 knockbackSpeed = new Vector2(8.0f, 4.0f);
    [Header("击退时间")] public float knockbackTime = 0.2f;
    private float startKnockbackTime; // 开始击退时间
    private bool isBeKnockback; // 是否正在被击退
    private int knockbackDirection; // 击退方向 1来自右边
    #endregion

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

    // 攻击动画回调 禁用转身
    public void DisableTurn()
    {
        if (!isDashing)
        {
            canTurn = false;
        }
    }

    // 攻击动画回调 启用转身
    public void EnableTurn()
    {
        if (!isDashing)
        {
            canTurn = true;
        }
    }

    // 受到伤害回调
    public void AcceptTouchDamage(float[] damageInfo)
    {
        if (!isDashing)
        {
            PlayerStates.Instance.DecreaseHealth(damageInfo[0]);
            Knockback(damageInfo[1]);
        }
    }

    // 受到近战攻击回调
    public void AcceptMeleeAttackDamage(AttackInfo attackInfo)
    {
        PlayerStates.Instance.DecreaseHealth(attackInfo.damage);
        Knockback(attackInfo.damageSourcePosX);
    }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        at = GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position, groundCheckBoxSize);
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(edgeCheck.position, new Vector2(edgeCheck.position.x + edgeCheckDistance, edgeCheck.position.y));
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
        CheckknockbackState();
        CheckSlidingWallState();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        Move();
        CheckEnvironment();
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

    // 检测冲刺状态
    private void CheckDashState()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canTurn = false;
                // 如果想水平冲刺不影响 归零刚体y轴速度
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

    // 检测跳跃按键
    private void CheckJumpInput()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.K))
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

        if (Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.K))
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
        // 墙上跳状态
        if (isTouchGround)
        {
            isWallJumping = false;
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

    // 检测击退状态
    private void CheckknockbackState()
    {
        if (isBeKnockback && Time.time >= startKnockbackTime + knockbackTime)
        {
            isBeKnockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
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
            // 冲刺时 如果在墙上 冲刺墙跳
            if (isTouchWall)
            {
                JumpInTheWall();
                Dash();
            }
            else
            {
                Dash();
            }
        }
        CheckJumpInput();
        CheckAbilityInput();
    }

    // 检测技能输入
    private void CheckAbilityInput()
    {
        if (Input.GetKeyDown(KeyCode.Y) && Time.time > lastDashTime + dashCoolDown)
        {
            Ability1();
        }
    }

    // 技能1 无影斩
    private void Ability1()
    {
        PlayerAttackController.Instance.StartAttack();
        Dash();
    }

    // 冲刺
    private void Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;
        dashTimeLeft = dashTimeMax;
        // 生成一个残影 记录当前残影X位置
        ObjectPool.Instance.GetObjFormPool();
        lastDashPosX = transform.position.x;
    }

    // 墙上跳下
    private void JumpFormWall()
    {
        isSlidingWall = false;
        currentJumpCount--;
        Vector2 forceAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
        rb.AddForce(forceAdd, ForceMode2D.Impulse);
    }

    // 在墙上跳跃
    private void JumpInTheWall()
    {
        isWallJumping = true;
        isSlidingWall = false;
        currentJumpCount--;
        Vector2 forceAdd = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
        rb.AddForce(forceAdd, ForceMode2D.Impulse);
        Turn();
    }

    // 移动
    private void Move()
    {
        // 地面
        if (isTouchGround && canMove && !isDashing && !isBeKnockback)
        {
            rb.velocity = new Vector2(horizontalDirection * moveSpeed, rb.velocity.y);
        }
        // 空中 控制
        else if (!isTouchGround && !isTouchWall && horizontalDirection != 0 && canMove && !isBeKnockback)
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

    // 正常跳跃
    private void NormalJump()
    {
        if (canJump && !isSlidingWall && !isWallJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            currentJumpCount--;
        }
    }

    // 转身
    private void Turn()
    {
        if (!isSlidingWall && canTurn && !isBeKnockback)
        {
            isFacingright = !isFacingright;
            facingDirection *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    // 受到击退
    public void Knockback(float direction)
    {
        isBeKnockback = true;
        startKnockbackTime = Time.time;
        if (direction < transform.position.x)
        {
            knockbackDirection = -1;
        }
        else
        {
            knockbackDirection = 1;
        }
        rb.velocity = new Vector2(knockbackSpeed.x * -knockbackDirection, knockbackSpeed.y);
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
}