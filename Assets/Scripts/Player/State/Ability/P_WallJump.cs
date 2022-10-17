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
        JudgeJumpDirection(sense.Wall());
        action.UseJumpInput();
        state.jump.DecreaseJumpCount();
        movement.SetVelocity(data.wallJumpForce, data.wallJumpAngle, wallJumpDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void JudgeJumpDirection(bool isTouchWall)
    {
        // 判断是否触墙 触墙则相反方向
        if (isTouchWall)
        {
            wallJumpDirection = -player.movement.facingDireciton;
            movement.SetTurn();
        }
        else
        {
            wallJumpDirection = player.movement.facingDireciton;
        }
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= startTime + data.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }
}