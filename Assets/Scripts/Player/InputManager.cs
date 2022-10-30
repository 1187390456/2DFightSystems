using EpicToonFX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public Gamepad gamepad { get => Gamepad.current; }
    public Keyboard keyboard { get => Keyboard.current; }

    public Mouse mouse { get => Mouse.current; }
    public Pointer pointer { get => Pointer.current; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (gamepad != null || keyboard != null)
        {
            CheckFire();
            CheckMove();
            CheckJump();
            CheckAttack();
            CheckDash();
            CheckSwitchPrevious();
            CheckSwitchNext();
            CheckYKeyInput();
            CheckHKeyInput();
            CheckCatch();
        }
        JumpFix();
        DashFix();
    }

    #region 射击

    private float lastFireTime;
    private float fireSpace = 0.2f;
    private bool isFireOn = false;

    private void CheckFire()
    {
        if (keyboard.nKey.wasPressedThisFrame)
        {
            isFireOn = !isFireOn;
        }

        if (isFireOn && Player.Instance.state.stateMachine.currentState != Player.Instance.state.dead)
        {
            if (Time.time >= lastFireTime + fireSpace)
            {
                ETFXFireProjectile.Instance.CreatePlayerProjectileWay();
                lastFireTime = Time.time;
            }
        }
    }

    #endregion 射击

    #region 抓取

    public bool catchInput;

    private void CheckCatch()
    {
        if (keyboard.tKey.wasPressedThisFrame)
        {
            catchInput = true;
        }
        if (keyboard.tKey.wasReleasedThisFrame)
        {
            catchInput = false;
        }
    }

    #endregion 抓取

    #region 移动

    public Vector2 movementInput { get; private set; }
    public int xInput { get; private set; }
    public int yInput { get; private set; }

    private void CheckMove()
    {
        if (gamepad.leftStick != null)
        {
            movementInput = gamepad.leftStick.ReadValue();
            xInput = Mathf.RoundToInt(movementInput.x);
            yInput = Mathf.RoundToInt(movementInput.y);
        }
        if (keyboard.aKey.isPressed && !keyboard.dKey.isPressed)
        {
            xInput = -(int)keyboard.aKey.ReadValue();
        }
        if (!keyboard.aKey.isPressed && keyboard.dKey.isPressed)
        {
            xInput = (int)keyboard.dKey.ReadValue();
        }
        if (keyboard.wKey.isPressed && !keyboard.sKey.isPressed)
        {
            yInput = (int)keyboard.wKey.ReadValue();
        }
        if (!keyboard.wKey.isPressed && keyboard.sKey.isPressed)
        {
            yInput = -(int)keyboard.sKey.ReadValue();
        }
    }

    #endregion 移动

    #region 跳跃

    public bool jumpInput;
    public bool jumpInputStop;
    private float jumpHoldTime = 0.2f;
    private float jumpTime;

    public void CheckJump()
    {
        if (keyboard.kKey.wasPressedThisFrame || keyboard.spaceKey.wasPressedThisFrame)
        {
            jumpInput = true;
            jumpTime = Time.time;
        }
        if (keyboard.kKey.wasReleasedThisFrame || keyboard.spaceKey.wasReleasedThisFrame)
        {
            jumpInputStop = true;
        }
    }

    private void JumpFix()
    {
        if (jumpInput && Time.time >= jumpTime + jumpHoldTime)
        {
            jumpInput = false;
        }
    }

    public void UseJumpInputStop() => jumpInputStop = false;

    public void UseJumpInput() => jumpInput = false;

    #endregion 跳跃

    #region 普通攻击

    public bool[] attackInput = new bool[Enum.GetValues(typeof(AttackInput)).Length];

    public void CheckAttack()
    {
        if (keyboard.jKey.wasPressedThisFrame)
        {
            attackInput[(int)AttackInput.first] = true;
        }
        if (keyboard.jKey.wasReleasedThisFrame)
        {
            attackInput[(int)AttackInput.first] = false;
        }
        if (keyboard.uKey.wasPressedThisFrame)
        {
            attackInput[(int)AttackInput.first] = true;
        }
        if (keyboard.uKey.wasReleasedThisFrame)
        {
            attackInput[(int)AttackInput.first] = false;
        }
    }

    #endregion 普通攻击

    #region 闪避

    public bool dashInput;
    public float dashStartTime;
    public float dashHoldTime = 0.2f;
    public bool dashInputStop;

    public void CheckDash()
    {
        if (keyboard.lKey.wasPressedThisFrame)
        {
            dashStartTime = Time.time;
            dashInput = true;
            dashInputStop = false;
        }
        if (keyboard.lKey.wasReleasedThisFrame)
        {
            dashInputStop = true;
        }
    }

    public void UseDashInput() => dashInput = false;

    public void DashFix()
    {
        if (dashInput && Time.time >= dashStartTime + dashHoldTime)
        {
            dashInput = false;
        }
    }

    #endregion 闪避

    #region 武器切换

    public bool switchPrevious;
    public bool switchNext;

    public void CheckSwitchPrevious()
    {
        if (keyboard.qKey.wasPressedThisFrame)
        {
            switchPrevious = true;
        }
        if (keyboard.qKey.wasReleasedThisFrame)
        {
            switchPrevious = false;
        }
    }

    public void CheckSwitchNext()
    {
        if (keyboard.eKey.wasPressedThisFrame)
        {
            switchNext = true;
        }
        if (keyboard.eKey.wasReleasedThisFrame)
        {
            switchNext = false;
        }
    }

    public void UseSwichPrevious() => switchPrevious = false;

    public void UseSwitchNext() => switchNext = false;

    #endregion 武器切换

    #region Y键 打开背包

    public bool yKeyInput;

    private void CheckYKeyInput()
    {
        if (keyboard.yKey.wasPressedThisFrame)
        {
            yKeyInput = true;
        }
        if (keyboard.yKey.wasReleasedThisFrame)
        {
            yKeyInput = false;
        }
    }

    public void UseYKey() => yKeyInput = false;

    #endregion Y键 打开背包

    #region H丢弃物品

    public bool hKeyInput;

    private void CheckHKeyInput()
    {
        if (keyboard.hKey.wasPressedThisFrame)
        {
            hKeyInput = true;
        }
        if (keyboard.hKey.wasReleasedThisFrame)
        {
            hKeyInput = false;
        }
    }

    public void UseHKey() => hKeyInput = false;

    #endregion H丢弃物品
}

public enum AttackInput
{
    first,
    second
}