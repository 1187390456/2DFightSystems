using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAction : CoreComponent
{
    public int GetXInput() => InputManager.Instance.xInput;

    public int GetYInput() => InputManager.Instance.yInput;

    public bool GetJumpInput() => InputManager.Instance.jumpInput;

    public bool GetJumpInputStop() => InputManager.Instance.jumpInputStop;

    public bool GetCatchInput() => InputManager.Instance.catchInput;

    public bool GetDashInput() => InputManager.Instance.dashInput;

    public bool GetDashInputStop() => InputManager.Instance.dashInputStop;

    public bool GetSwitchPrevious() => InputManager.Instance.switchPrevious;

    public bool GetSwitchNext() => InputManager.Instance.switchNext;

    public bool GetYKeyInput() => InputManager.Instance.yKeyInput;

    public bool GetHKeyInput() => InputManager.Instance.hKeyInput;

    public Vector2 GetDashDirtion() => InputManager.Instance.movementInput;

    public void UseDashInput() => InputManager.Instance.UseDashInput();

    public void UseJumpInput() => InputManager.Instance.UseJumpInput();

    public void UseSwitchPrevious() => InputManager.Instance.UseSwichPrevious();

    public void UseSwitchNext() => InputManager.Instance.UseSwitchNext();

    public void UseYKeyInput() => InputManager.Instance.UseYKey();

    public void UseHKeyInput() => InputManager.Instance.UseHKey();

    public void UseJumpInputStop() => InputManager.Instance.UseJumpInputStop();
}