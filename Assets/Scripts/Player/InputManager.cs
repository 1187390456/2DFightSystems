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
    public Vector2 movementInput { get; private set; }
    public int inputX { get; private set; }
    public int inputY { get; private set; }

    public bool isJump { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        inputX = (int)(movementInput * Vector2.right).normalized.x;
        inputY = (int)(movementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayerController.Instance.JumpButtonDown();
        }
        if (context.canceled)
        {
            PlayerController.Instance.JumpButtonUp();
        }
    }

    public void OnFireInput(InputAction.CallbackContext context)
    {
        // MoveToGameManager ETFXFireProjectile.Instance.CreatePlayerProjectileWay2();
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayerAttackController.Instance.StartAttack();
        }
    }

    public void OnDodgeInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayerController.Instance.Dash();
        }
    }

    public void OnSwitchLeftInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ETFXFireProjectile.Instance.previousEffect();
        }
    }

    public void OnSwitchRightInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ETFXFireProjectile.Instance.nextEffect();
        }
    }
}