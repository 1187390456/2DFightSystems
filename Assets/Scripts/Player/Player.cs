using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region 核心

    public Core core { get; private set; }
    public Movement movement => core.movement;
    public CollisionSenses sense => core.collisionSenses;
    public InputAction action => core.inputAction;

    #endregion 核心

    [Header("玩家数据")] public D_P_Base playerData;
    public static Player Instance { get; private set; }

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

    #endregion 其他

    #region Unity回调

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        at = GetComponent<Animator>();
        collider2d = GetComponent<BoxCollider2D>();
        weaponInventory = GetComponent<WeaponInventory>();
        core = transform.Find("Core").GetComponent<Core>();
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
    }

    private void Start()
    {
        firstAttack.SetWeapon(weaponInventory.weapons[0]);
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

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    #endregion Unity回调

    #region 回调函数

    private void StartAnimation() => stateMachine.currentState.StartAnimation();

    private void FinishAnimation() => stateMachine.currentState.FinishAnimation();

    #endregion 回调函数

    #region 设置

    public void SetDestory() => Destroy(gameObject);

    public void SetHoldStatic(Vector2 holdPos)
    {
        transform.position = holdPos;
        movement.SetVelocityZero();
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

    public bool GroundCondition() => rb.velocity.y <= 0.01f && sense.Ground();

    public bool DashCondition() => dash.CheckCanDash() && action.GetDashInput() && stateMachine.currentState != crouchIdle && stateMachine.currentState != crouchMove;

    public bool JumpCondition() => InputManager.Instance.jumpInput && jump.ChechCanJump();

    public bool LedgeCondition() => !sense.Ledge() && sense.Wall() && !sense.Ground();

    public bool CatchWallConditon() => sense.Wall() && action.GetCatchInput() && sense.Ledge();

    public bool FirstAttackCondition() => InputManager.Instance.attackInput[(int)AttackInput.first] && !sense.Top();

    public bool SecondAttackCondition() => InputManager.Instance.attackInput[(int)AttackInput.second] && !sense.Top();

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