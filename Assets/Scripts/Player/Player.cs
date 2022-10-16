using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [Header("地面检测点")] public Transform groundCheck;
    [Header("墙壁检测点")] public Transform wallCheck;
    [Header("边缘检测点")] public Transform ledgeCheck;
    [Header("顶部检测点")] public Transform topCheck;
    [Header("玩家数据")] public D_P_Base playerData;

    public P_StateMachine stateMachine = new P_StateMachine();
    public Rigidbody2D rb { get; private set; }
    public BoxCollider2D collider2d { get; private set; }
    public Animator at { get; private set; }
    public WeaponInventory weaponInventory { get; private set; }

    #region UI

    public GameObject canvas { get; private set; }
    public Text health { get; private set; }
    public GameObject deadTimer { get; private set; }

    #endregion UI

    public InputManager inputManager { get; private set; }

    public int facingDireciton { get; private set; }

    public GameObject dashIndicator { get; private set; }

    public Vector2 normalColliderSize { get; private set; }
    public Vector2 normalColliderOffset { get; private set; }
    public int knockBackDirection { get; private set; }

    [HideInInspector] public float currentHealth;
    [HideInInspector] public Vector2 workSpace;

    #region 状态

    public P_Idle idle { get; private set; }
    public P_Move move { get; private set; }
    public P_Jump jump { get; private set; }
    public P_InAir inAir { get; private set; }
    public P_Land land { get; private set; }
    public P_Silde slide { get; private set; }
    public P_Catch catchWall { get; private set; }
    public P_Climb climb { get; private set; }
    public P_WallJump wallJump { get; private set; }

    public P_Ledge ledge { get; private set; }
    public P_Dash dash { get; private set; }
    public P_CrouchIdle crouchIdle { get; private set; }
    public P_CrouchMove crouchMove { get; private set; }
    public P_KnockBack knockBack { get; private set; }
    public P_Dead dead { get; private set; }

    public P_Attack firstAttack { get; private set; }
    public P_Attack secondAttack { get; private set; }

    #endregion 状态

    #region 其他

    public void TransportAttackInfoToEnemy(RaycastHit2D objs, Transform trans)
    {
        AttackInfo attackInfo = new AttackInfo()
        {
            damage = 10.0f,
            damageSourcePosX = trans.position.x
        };
        objs.transform.parent.SendMessage("AcceptPlayerDamage", attackInfo);
    }

    public void AcceptAttackDamage(AttackInfo attackInfo)
    {
        currentHealth -= attackInfo.damage;
        health.text = $"当前生命值为 : {currentHealth}";

        if (currentHealth <= 0)
        {
            stateMachine.ChangeState(dead);
        }
        else
        {
            knockBackDirection = CheckKnockBackDirection(attackInfo.damageSourcePosX);
            if (stateMachine.currentState != knockBack)
            {
                stateMachine.ChangeState(knockBack);
            }
        }
    }

    private void UpdateAnimation()
    {
        at.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        at.SetFloat("yVelocity", rb.velocity.y);
    }

    public Vector2 ComputedCornerPos()
    {
        var xhit = Physics2D.Raycast(wallCheck.position, transform.right, playerData.wallCheckDistance, LayerMask.GetMask("Ground"));
        var xDis = xhit.distance;
        workSpace.Set(playerData.wallCheckDistance * facingDireciton, 0);
        var yhit = Physics2D.Raycast(ledgeCheck.position + (Vector3)workSpace, Vector2.down, ledgeCheck.position.y - wallCheck.position.y, LayerMask.GetMask("Ground"));
        var yDis = yhit.distance;
        workSpace.Set(wallCheck.position.x + xDis * facingDireciton, ledgeCheck.position.y - yDis);
        return workSpace;
    }

    #endregion 其他

    #region Unity回调

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        at = GetComponent<Animator>();
        collider2d = GetComponent<BoxCollider2D>();
        inputManager = GetComponent<InputManager>();
        weaponInventory = GetComponent<WeaponInventory>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        health = canvas.transform.Find("Health").GetComponent<Text>();
        deadTimer = canvas.transform.Find("DeadTimer").gameObject;
        normalColliderSize = collider2d.size;
        normalColliderOffset = collider2d.offset;

        dashIndicator = transform.Find("DashDirectionIndicator").gameObject;

        idle = new P_Idle(stateMachine, this, "idle", playerData);
        move = new P_Move(stateMachine, this, "move", playerData);
        jump = new P_Jump(stateMachine, this, "inAir", playerData);
        inAir = new P_InAir(stateMachine, this, "inAir", playerData);
        land = new P_Land(stateMachine, this, "land", playerData);
        slide = new P_Silde(stateMachine, this, "silde", playerData);
        catchWall = new P_Catch(stateMachine, this, "catch", playerData);
        climb = new P_Climb(stateMachine, this, "climb", playerData);
        wallJump = new P_WallJump(stateMachine, this, "inAir", playerData);
        ledge = new P_Ledge(stateMachine, this, "ledgeState", playerData);
        dash = new P_Dash(stateMachine, this, "inAir", playerData);
        crouchIdle = new P_CrouchIdle(stateMachine, this, "crouchIdle", playerData);
        crouchMove = new P_CrouchMove(stateMachine, this, "crouchMove", playerData);
        knockBack = new P_KnockBack(stateMachine, this, "knockBack", playerData);
        dead = new P_Dead(stateMachine, this, "dead", playerData);
        firstAttack = new P_Attack(stateMachine, this, "attack", playerData);
        secondAttack = new P_Attack(stateMachine, this, "attack", playerData);
        stateMachine.Init(idle);

        facingDireciton = 1;
    }

    private void Start()
    {
        firstAttack.SetWeapon(weaponInventory.weapons[(int)AttackInput.first]);
        dashIndicator.gameObject.SetActive(false);
        SetDeadTimer(false);
        currentHealth = playerData.maxHealth;
        health.text = $"当前生命值为 : {currentHealth}";
        CheckEnvironment();
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        UpdateAnimation();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position, playerData.groundCheckSize);
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerData.wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x - playerData.wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(ledgeCheck.position, new Vector2(ledgeCheck.position.x + playerData.wallCheckDistance, ledgeCheck.position.y));
        Gizmos.DrawWireSphere(topCheck.position, playerData.topCheckRadius);
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    #endregion Unity回调

    #region InputManager

    public int GetXInput() => InputManager.Instance.xInput;

    public int GetYInput() => InputManager.Instance.yInput;

    public bool GetJumpInputStop() => InputManager.Instance.jumpInputStop;

    public bool GetCatchInput() => InputManager.Instance.catchInput;

    public bool GetDashInput() => InputManager.Instance.dashInput;

    public bool GetDashInputStop() => InputManager.Instance.dashInputStop;

    public Vector2 GetDashDirtion() => InputManager.Instance.movementInput;

    public void UseDashInput() => InputManager.Instance.UseDashInput();

    public void UseJumpInput() => InputManager.Instance.UseJumpInput();

    public void UseJumpInputStop() => InputManager.Instance.UseJumpInputStop();

    #endregion InputManager

    #region 回调函数

    private void StartAnimation() => stateMachine.currentState.StartAnimation();

    private void FinishAnimation() => stateMachine.currentState.FinishAnimation();

    #endregion 回调函数

    #region 设置

    public void SetDestory() => Destroy(gameObject);

    public void SetVelocityX(float velocity) => rb.velocity = new Vector2(velocity, rb.velocity.y);

    public void SetVelocitY(float velocity) => rb.velocity = new Vector2(rb.velocity.x, velocity);

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        rb.velocity = new Vector2(velocity * angle.x * direction, velocity * angle.y);
    }

    public void SetVelocity(float velocity, Vector2 direction) => rb.velocity = direction * velocity;

    public void SetVelocityZero() => rb.velocity = Vector2.zero;

    public void SetPlayerMove(float velocity) => SetVelocityX(velocity * GetXInput());

    public void SetTurn()
    {
        facingDireciton *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void SetHoldStatic(Vector2 holdPos)
    {
        transform.position = holdPos;
        SetVelocityZero();
    }

    public void SetHalfCollider()
    {
        var offset = collider2d.offset;
        var size = collider2d.size;
        workSpace.Set(size.x, size.y / 2);

        offset.y += (size.y / 2 - size.y) / 2;
        collider2d.size = workSpace;
        collider2d.offset = offset;
    }

    public void SetResumeCollider()
    {
        collider2d.size = normalColliderSize;
        collider2d.offset = normalColliderOffset;
    }

    public void SetCanvasBtnState(bool value)
    {
        canvas.transform.Find("button").gameObject.SetActive(value);
        canvas.transform.Find("Switch").gameObject.SetActive(value);
        canvas.transform.Find("move").gameObject.SetActive(value);
    }

    public void SetDeadTimer(bool value) => deadTimer.gameObject.SetActive(value);

    #endregion 设置

    #region 检测状态

    public void CheckTurn()
    {
        if (facingDireciton == 1 && inputManager.xInput < 0)
        {
            SetTurn();
        }
        else if (facingDireciton == -1 && inputManager.xInput > 0)
        {
            SetTurn();
        }
    }

    public bool CheckGround() => Physics2D.BoxCast(groundCheck.position, playerData.groundCheckSize, 0.0f, transform.right, 0.0f, LayerMask.GetMask("Ground"));

    public bool ChechWall() => Physics2D.Raycast(wallCheck.position, transform.right, playerData.wallCheckDistance, LayerMask.GetMask("Ground"));

    public bool CheckBackWall() => Physics2D.Raycast(wallCheck.position, -transform.right, playerData.wallCheckDistance, LayerMask.GetMask("Ground"));

    public bool CheckLedge() => Physics2D.Raycast(ledgeCheck.position, transform.right, playerData.wallCheckDistance, LayerMask.GetMask("Ground"));

    public bool CheckTop() => Physics2D.OverlapCircle(topCheck.position, playerData.topCheckRadius, LayerMask.GetMask("Ground"));

    public bool GroundCondition() => rb.velocity.y <= 0.01f && CheckGround();

    public bool DashCondition() => dash.CheckCanDash() && GetDashInput() && stateMachine.currentState != crouchIdle && stateMachine.currentState != crouchMove;

    public bool JumpCondition() => InputManager.Instance.jumpInput && jump.ChechCanJump();

    public bool LedgeCondition() => !CheckLedge() && ChechWall() && !CheckGround();

    public bool CatchWallConditon() => ChechWall() && GetCatchInput() && CheckLedge();

    public bool FirstAttackCondition() => InputManager.Instance.attackInput[(int)AttackInput.first] && !CheckTop();

    public bool SecondAttackCondition() => InputManager.Instance.attackInput[(int)AttackInput.second] && !CheckTop();

    public int CheckKnockBackDirection(float direction) => direction < transform.position.x ? 1 : -1;

    public void CheckEnvironment()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            SetCanvasBtnState(true);
        }
        else
        {
            SetCanvasBtnState(false);
        }
    }

    #endregion 检测状态
}