using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAction : CoreComponent
{
    public int GetXInput() => InputManager.Instance.xInput;

    public int GetYInput() => InputManager.Instance.yInput;

    public bool GetJumpInputStop() => InputManager.Instance.jumpInputStop;

    public bool GetCatchInput() => InputManager.Instance.catchInput;

    public bool GetDashInput() => InputManager.Instance.dashInput;

    public bool GetDashInputStop() => InputManager.Instance.dashInputStop;

    public Vector2 GetDashDirtion() => InputManager.Instance.movementInput;

    public void UseDashInput() => InputManager.Instance.UseDashInput();

    public void UseJumpInput() => InputManager.Instance.UseJumpInput();

    public void UseJumpInputStop() => InputManager.Instance.UseJumpInputStop();
}