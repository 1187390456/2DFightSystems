using InventorySystem;
using InventorySystem.UI;
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region 核心

    public Core core { get; private set; }

    public Movement movement
    {
        get
        {
            if (core == null)
            {
                core = transform.Find("Core").GetComponent<Core>();
            }
            return core.movement;
        }
    }

    public PlayerCollisionSenses sense
    {
        get
        {
            if (core == null)
            {
                core = transform.Find("Core").GetComponent<Core>();
            }
            return core.playerCollisionSenses;
        }
    }

    public InputAction action
    {
        get
        {
            if (core == null)
            {
                core = transform.Find("Core").GetComponent<Core>();
            }
            return core.inputAction;
        }
    }

    public PlayerState state
    {
        get
        {
            if (core == null)
            {
                if (transform == null) throw new Exception("玩家已销毁!");
                core = transform.Find("Core").GetComponent<Core>();
            }
            return core.playerState;
        }
    }

    public PlayerStats stats
    {
        get
        {
            if (core == null)
            {
                core = transform.Find("Core").GetComponent<Core>();
            }
            return core.playerStats;
        }
    }

    #endregion 核心

    #region 基本组件与属性

    public P_StateMachine stateMachine => state.stateMachine;
    public static Player Instance { get; private set; }

    public Animator at
    {
        get => GetComponent<Animator>();
    }

    public WeaponInventory weaponInventory { get; private set; }
    public GameObject dashIndicator { get; private set; }
    public int currentWeaponIndex { get; private set; }
    public Inventory _inventory { get; private set; }
    public bool inInventorying { get; set; }

    #endregion 基本组件与属性

    private void Awake()
    {
        Instance = this;
        core = transform.Find("Core").GetComponent<Core>();
        _inventory = transform.Find("Inventory").GetComponent<Inventory>();
        weaponInventory = GetComponent<WeaponInventory>();
        dashIndicator = transform.Find("DashDirectionIndicator").gameObject;
        dashIndicator.gameObject.SetActive(false);
        currentWeaponIndex = 0;
        inInventorying = true;
    }

    private void Start()
    {
        state.firstAttack.SetWeapon(weaponInventory.weapons[currentWeaponIndex]);
    }

    private void Update()
    {
        at.SetFloat("xVelocity", Mathf.Abs(movement.rbX));
        at.SetFloat("yVelocity", movement.rbY);
        CheckInput();
    }

    private void CheckInput()
    {
        if (inInventorying)
        {
            if (action.GetSwitchPrevious())
            {
                action.UseSwitchPrevious();
                SwitchPreviousInventoryIndex();
            }
            if (action.GetSwitchNext())
            {
                action.UseSwitchNext();
                SwitchNextInventoryIndex();
            }
            if (action.GetHKeyInput())
            {
                action.UseHKeyInput();
                if (_inventory.GetInventorySolt().HasItem)
                {
                    _inventory.RemoveItem(_inventory.ActiveSlotIndex, true);
                }
            }
        }
        else
        {
            if (action.GetSwitchPrevious() && state.stateMachine.currentState != state.firstAttack)
            {
                action.UseSwitchPrevious();
                SwitchPreviousWeapon();
            }
            if (action.GetSwitchNext() && state.stateMachine.currentState != state.firstAttack)
            {
                action.UseSwitchNext();
                SwitchNextWeapon();
            }
        }
        if (action.GetYKeyInput())
        {
            action.UseYKeyInput();
            OpenInventory();
        }
    }

    #region 操作

    public void SwitchNextWeapon()
    {
        currentWeaponIndex = Mathf.Clamp(currentWeaponIndex + 1, 0, weaponInventory.weapons.Length - 1);
        state.firstAttack.SetWeapon(weaponInventory.weapons[currentWeaponIndex]);
    }

    public void SwitchPreviousWeapon()
    {
        currentWeaponIndex = Mathf.Clamp(currentWeaponIndex - 1, 0, weaponInventory.weapons.Length - 1);
        state.firstAttack.SetWeapon(weaponInventory.weapons[currentWeaponIndex]);
    }

    public void SwitchNextInventoryIndex() => _inventory.SetSlotIndex(_inventory.ActiveSlotIndex + 1);

    public void SwitchPreviousInventoryIndex() => _inventory.SetSlotIndex(_inventory.ActiveSlotIndex - 1);

    public void OpenInventory() => UI_Inventory.Instance.OpenInventory();

    #endregion 操作

    private void StartAnimation() => stateMachine.currentState.StartAnimation();

    private void FinishAnimation() => stateMachine.currentState.FinishAnimation();

    public void SetDestory() => Destroy(gameObject);

    public bool GroundCondition() => movement.rbY <= 0.01f && sense.Ground();

    public bool DashCondition() => state.dash.CheckCanDash() && action.GetDashInput() && state.stateMachine.currentState != state.crouchIdle && state.stateMachine.currentState != state.crouchMove;

    public bool JumpCondition() => InputManager.Instance.jumpInput && state.jump.ChechCanJump() && state.stateMachine.currentState != state.crouchIdle && state.stateMachine.currentState != state.crouchMove;

    public bool LedgeCondition() => !sense.Ledge() && sense.Wall() && !sense.Ground();

    public bool CatchWallConditon() => sense.Wall() && action.GetCatchInput() && sense.Ledge();

    public bool FirstAttackCondition() => InputManager.Instance.attackInput[(int)AttackInput.first] && !sense.Top();

    public bool SecondAttackCondition() => InputManager.Instance.attackInput[(int)AttackInput.second] && !sense.Top();
}