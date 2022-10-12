using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Wall : P_State
{
    public P_Wall(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.jump.ResetJumpCount();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void StartAnimation()
    {
        base.StartAnimation();
    }

    public override void Update()
    {
        base.Update();
        if (!player.CheckLedge() && player.ChechWall())
        {
            stateMachine.ChangeState(player.ledge);
        }
        else if (player.JumpCondition())
        {
            stateMachine.ChangeState(player.wallJump);
        }
        else if (player.GroundCondition() && !player.GetCatchInput())
        {
            if (player.GetXInput() != 0)
            {
                stateMachine.ChangeState(player.move);
            }
            else
            {
                stateMachine.ChangeState(player.idle);
            }
        }
        else if (!player.ChechWall() || (player.GetXInput() != player.facingDireciton && !player.GetCatchInput()))
        {
            stateMachine.ChangeState(player.inAir);
        }
    }
}