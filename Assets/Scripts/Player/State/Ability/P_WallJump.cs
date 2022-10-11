using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_WallJump : P_Ability
{
    protected int wallJumpDirection;

    public P_WallJump(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        JudgeJumpDirection(player.ChechWall());
        player.UseJumpInput();
        player.jump.ResetJumpCount();
        player.jump.DecreaseJumpCount();
        player.SetVelocity(playerData.wallJumpForce, playerData.wallJumpAngle, wallJumpDirection);
    }

    public override void Exit()
    {
        base.Exit();
        player.inAir.StartWallJumpGraceTime();
    }

    public void JudgeJumpDirection(bool isTouchWall)
    {
        // 判断是否触墙 触墙则相反方向
        if (isTouchWall)
        {
            wallJumpDirection = -player.facingDireciton;
            player.SetTurn();
        }
        else
        {
            wallJumpDirection = player.facingDireciton;
        }
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }
}