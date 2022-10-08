using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Jump : P_Ability
{
    public int currentJumpCount;

    public P_Jump(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
        ResetJumpCount();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocitY(playerData.jumpForce);
        currentJumpCount--;
        isAbilityDone = true;
    }

    public void ResetJumpCount() => currentJumpCount = playerData.jumpCount;

    public bool ChechCanJump() => currentJumpCount > 0;
}