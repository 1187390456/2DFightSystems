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
    public PlayerStats stats => core.playerStats;

    #endregion 核心

    #region 基本组件与属性

    public P_StateMachine stateMachine => state.stateMachine;
    public static Player Instance { get; private set; }
    public Animator at { get; private set; }
    public WeaponInventory weaponInventory { get; private set; }
    public GameObject dashIndicator { get; private set; }

    #endregion 基本组件与属性

    private void Awake()
    {
        Instance = this;
        core = transform.Find("Core").GetComponent<Core>();
        at = GetComponent<Animator>();
        weaponInventory = GetComponent<WeaponInventory>();
        dashIndicator = transform.Find("DashDirectionIndicator").gameObject;
        dashIndicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        at.SetFloat("xVelocity", Mathf.Abs(movement.rbX));
        at.SetFloat("yVelocity", movement.rbY);
    }

    private void StartAnimation() => stateMachine.currentState.StartAnimation();

    private void FinishAnimation() => stateMachine.currentState.FinishAnimation();

    public void SetDestory() => Destroy(gameObject);

    public bool GroundCondition() => movement.rbY <= 0.01f && sense.Ground();

    public bool DashCondition() => state.dash.CheckCanDash() && action.GetDashInput() && state.stateMachine.currentState != state.crouchIdle && state.stateMachine.currentState != state.crouchMove;

    public bool JumpCondition() => InputManager.Instance.jumpInput && state.jump.ChechCanJump();

    public bool LedgeCondition() => !sense.Ledge() && sense.Wall() && !sense.Ground();

    public bool CatchWallConditon() => sense.Wall() && action.GetCatchInput() && sense.Ledge();

    public bool FirstAttackCondition() => InputManager.Instance.attackInput[(int)AttackInput.first] && !sense.Top();

    public bool SecondAttackCondition() => InputManager.Instance.attackInput[(int)AttackInput.second] && !sense.Top();
}