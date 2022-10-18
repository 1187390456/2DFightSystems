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
    public Combat combat => core.combat;

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

    private void UpdateAnimation()
    {
        at.SetFloat("xVelocity", Mathf.Abs(movement.rbX));
        at.SetFloat("yVelocity", movement.rbY);
    }

    private void Awake()
    {
        Instance = this;
        at = GetComponent<Animator>();

        core = transform.Find("Core").GetComponent<Core>();
        weaponInventory = GetComponent<WeaponInventory>();
        dashIndicator = transform.Find("DashDirectionIndicator").gameObject;
    }

    private void Start()
    {
        dashIndicator.gameObject.SetActive(false);

        currentHealth = state.playerData.maxHealth;
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void StartAnimation() => state.stateMachine.currentState.StartAnimation();

    private void FinishAnimation() => state.stateMachine.currentState.FinishAnimation();

    public void SetDestory() => Destroy(gameObject);

    public bool GroundCondition() => movement.rbY <= 0.01f && sense.Ground();

    public bool DashCondition() => state.dash.CheckCanDash() && action.GetDashInput() && state.stateMachine.currentState != state.crouchIdle && state.stateMachine.currentState != state.crouchMove;

    public bool JumpCondition() => InputManager.Instance.jumpInput && state.jump.ChechCanJump();

    public bool LedgeCondition() => !sense.Ledge() && sense.Wall() && !sense.Ground();

    public bool CatchWallConditon() => sense.Wall() && action.GetCatchInput() && sense.Ledge();

    public bool FirstAttackCondition() => InputManager.Instance.attackInput[(int)AttackInput.first] && !sense.Top();

    public bool SecondAttackCondition() => InputManager.Instance.attackInput[(int)AttackInput.second] && !sense.Top();

    public int CheckKnockBackDirection(float direction) => direction < transform.position.x ? 1 : -1;
}