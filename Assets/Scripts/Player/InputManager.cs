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
    }

    #region …‰ª˜

    private float lastFireTime;
    private float fireSpace = 0.2f;
    private bool isFireOn = false;

    // ºÏ≤‚…‰ª˜
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

    #endregion …‰ª˜

    #region “∆∂Ø

    public Vector2 movementInput { get; private set; }
    public int inputX { get; private set; }
    public int inputY { get; private set; }

    private void CheckMove()
    {
        movementInput = gamepad.leftStick.ReadValue();
        inputX = (int)(movementInput * Vector2.right).normalized.x;
        inputY = (int)(movementInput * Vector2.up).normalized.y;
    }

    #endregion “∆∂Ø

    #region Ã¯‘æ

    public void CheckJump()
    {
        if (gamepad.buttonSouth.wasPressedThisFrame)
        {
            PlayerController.Instance.JumpButtonDown();
        }
        if (gamepad.buttonSouth.wasReleasedThisFrame)
        {
            PlayerController.Instance.JumpButtonUp();
        }
    }

    #endregion Ã¯‘æ

    #region ∆’Õ®π•ª˜

    public void CheckAttack()
    {
        if (gamepad.buttonEast.wasPressedThisFrame)
        {
            PlayerAttackController.Instance.StartAttack();
        }
    }

    #endregion ∆’Õ®π•ª˜

    #region …¡±‹

    public void CheckDodge()
    {
        if (gamepad.buttonNorth.wasPressedThisFrame)
        {
            PlayerController.Instance.Dash();
        }
    }

    #endregion …¡±‹

    #region ◊Û”“Ãÿ–ß«–ªª

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

    #endregion ◊Û”“Ãÿ–ß«–ªª
}