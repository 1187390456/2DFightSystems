using EpicToonFX;
using System.Collections;
using System.Collections.Generic;
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
        if (gamepad != null)
        {
            CheckFire();
            CheckMove();
            CheckJump();
            CheckAttack();
            CheckDodge();
            CheckSwitchLeft();
            CheckSwitchRight();
        }
        JumpFix();
    }

    #region 射击

    private float lastFireTime;
    private float fireSpace = 0.2f;
    private bool isFireOn = false;

    // 检测射击
    private void CheckFire()
    {
        if (gamepad.buttonWest.wasPressedThisFrame)
        {
            isFireOn = !isFireOn;
        }

        if (isFireOn && !PlayerStates.Instance.isDead)
        {
            if (Time.time >= lastFireTime + fireSpace)
            {
                ETFXFireProjectile.Instance.CreatePlayerProjectileWay2();
                lastFireTime = Time.time;
            }
        }
    }

    #endregion 射击

    #region 移动

    public Vector2 movementInput { get; private set; }
    public int xInput { get; private set; }
    public int yInput { get; private set; }

    private void CheckMove()
    {
        if (gamepad.leftStick != null)
        {
            movementInput = gamepad.leftStick.ReadValue();
            xInput = (int)(movementInput * Vector2.right).normalized.x;
            yInput = (int)(movementInput * Vector2.up).normalized.y;
        }
        if (keyboard.aKey.isPressed && !keyboard.dKey.isPressed)
        {
            xInput = -(int)keyboard.aKey.ReadValue();
        }
        if (!keyboard.aKey.isPressed && keyboard.dKey.isPressed)
        {
            xInput = (int)keyboard.dKey.ReadValue();
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
        if (gamepad.buttonSouth.wasPressedThisFrame || keyboard.kKey.wasPressedThisFrame || keyboard.spaceKey.wasPressedThisFrame)
        {
            jumpInput = true;
            jumpTime = Time.time;
        }
        if (gamepad.buttonSouth.wasReleasedThisFrame || keyboard.kKey.wasReleasedThisFrame || keyboard.spaceKey.wasReleasedThisFrame)
        {
            jumpInputStop = true;
        }
    }

    private void JumpFix()
    {
        if (Time.time >= jumpTime + jumpHoldTime)
        {
            jumpInput = false;
        }
    }

    public void UseJumpInputStop() => jumpInputStop = false;

    public void UseJumpInput() => jumpInput = false;

    #endregion 跳跃

    #region 普通攻击

    public void CheckAttack()
    {
        if (gamepad.buttonEast.wasPressedThisFrame)
        {
            PlayerAttackController.Instance.StartAttack();
        }
    }

    #endregion 普通攻击

    #region 闪避

    public void CheckDodge()
    {
        if (gamepad.buttonNorth.wasPressedThisFrame)
        {
            PlayerController.Instance.Dash();
        }
    }

    #endregion 闪避

    #region 左右特效切换

    public void CheckSwitchLeft()
    {
        if (gamepad.dpad.left.wasPressedThisFrame)
        {
            ETFXFireProjectile.Instance.previousEffect();
        }
    }

    public void CheckSwitchRight()
    {
        if (gamepad.dpad.right.wasPressedThisFrame)
        {
            ETFXFireProjectile.Instance.nextEffect();
        }
    }

    #endregion 左右特效切换
}