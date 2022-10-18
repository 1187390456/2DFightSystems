using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region 核心

    public Core core { get; private set; }
    public Movement movement => core.movement;
    public PlayerCollisionSenses sense => core.playerCollisionSenses;
    public InputAction action => core.inputAction;

    public PlayerState state => core.playerState;

    #endregion 核心

    #region 基本组件与属性

    public static Player Instance { get; private set; }
    public Animator at { get; private set; }
    public WeaponInventory weaponInventory { get; private set; }
    public GameObject dashIndicator { get; private set; }

    public int knockBackDirection { get; private set; }

    [HideInInspector] public float currentHealth;
    [HideInInspector] public Vector2 workSpace;

    #endregion 基本组件与属性

    #region UI

    public GameObject canvas { get; private set; }
    public Text health { get; private set; }
    public GameObject deadTimer { get; private set; }

    #endregion UI

    #region 其他

    public void TransportAttackInfoToEnemy(RaycastHit2D objs, Transform trans)
    {
        AttackInfo attackInfo = new AttackInfo()
        {
            damage = 10.0f,
            damageSourcePosX = trans.position.x
        };
        objs.transform.SendMessage("AcceptPlayerDamage", attackInfo);
    }

    public void AcceptAttackDamage(AttackInfo attackInfo)
    {
        currentHealth -= attackInfo.damage;
        health.text = $"当前生命值为 : {currentHealth}";

        if (currentHealth <= 0)
        {
            state.stateMachine.ChangeState(state.dead);
        }
        else
        {
            knockBackDirection = CheckKnockBackDirection(attackInfo.damageSourcePosX);
            if (state.stateMachine.currentState != state.knockBack)
            {
                state.stateMachine.ChangeState(state.knockBack);
            }
        }
    }

    private void UpdateAnimation()
    {
        at.SetFloat("xVelocity", Mathf.Abs(movement.rbX));
        at.SetFloat("yVelocity", movement.rbY);
    }

    #endregion 其他

    #region Unity回调

    private void Awake()
    {
        Instance = this;
        at = GetComponent<Animator>();

        core = transform.Find("Core").GetComponent<Core>();
        weaponInventory = GetComponent<WeaponInventory>();

        canvas = GameObject.FindGameObjectWithTag("Canvas");
        health = canvas.transform.Find("Health").GetComponent<Text>();
        deadTimer = canvas.transform.Find("DeadTimer").gameObject;

        dashIndicator = transform.Find("DashDirectionIndicator").gameObject;
    }

    private void Start()
    {
        dashIndicator.gameObject.SetActive(false);
        SetDeadTimer(false);
        currentHealth = state.playerData.maxHealth;
        health.text = $"当前生命值为 : {currentHealth}";
        if (sense.Android())
        {
            SetCanvasBtnState(true);
        }
        else
        {
            SetCanvasBtnState(false);
        }
    }

    private void Update()
    {
        UpdateAnimation();
    }

    #endregion Unity回调

    #region 回调函数

    private void StartAnimation() => state.stateMachine.currentState.StartAnimation();

    private void FinishAnimation() => state.stateMachine.currentState.FinishAnimation();

    #endregion 回调函数

    #region 设置

    public void SetDestory() => Destroy(gameObject);

    public void SetCanvasBtnState(bool value)
    {
        canvas.transform.Find("button").gameObject.SetActive(value);
        canvas.transform.Find("Switch").gameObject.SetActive(value);
        canvas.transform.Find("move").gameObject.SetActive(value);
    }

    public void SetDeadTimer(bool value) => deadTimer.gameObject.SetActive(value);

    #endregion 设置

    #region 过度条件

    public bool GroundCondition() => movement.rbY <= 0.01f && sense.Ground();

    public bool DashCondition() => state.dash.CheckCanDash() && action.GetDashInput() && state.stateMachine.currentState != state.crouchIdle && state.stateMachine.currentState != state.crouchMove;

    public bool JumpCondition() => InputManager.Instance.jumpInput && state.jump.ChechCanJump();

    public bool LedgeCondition() => !sense.Ledge() && sense.Wall() && !sense.Ground();

    public bool CatchWallConditon() => sense.Wall() && action.GetCatchInput() && sense.Ledge();

    public bool FirstAttackCondition() => InputManager.Instance.attackInput[(int)AttackInput.first] && !sense.Top();

    public bool SecondAttackCondition() => InputManager.Instance.attackInput[(int)AttackInput.second] && !sense.Top();

    public int CheckKnockBackDirection(float direction) => direction < transform.position.x ? 1 : -1;

    #endregion 过度条件
}