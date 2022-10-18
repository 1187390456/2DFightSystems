using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Jump : P_Ability
{
    protected int currentJumpCount;

    public P_Jump(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
        ResetJumpCount();
    }

    public override void Enter()
    {
        base.Enter();
        action.UseJumpInput();
        DecreaseJumpCount();
        movement.SetVelocityY(data.jumpForce);
        isAbilityDone = true;
    }

    public void ResetJumpCount() => currentJumpCount = data.jumpCount;

    public bool ChechCanJump() => currentJumpCount > 0;

    public void DecreaseJumpCount() => currentJumpCount--;
}