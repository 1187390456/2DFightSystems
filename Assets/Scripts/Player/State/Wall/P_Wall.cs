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
        state.jump.ResetJumpCount();
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
        if (player.LedgeCondition())
        {
            stateMachine.ChangeState(state.ledge);
        }
        else if (player.JumpCondition())
        {
            stateMachine.ChangeState(state.wallJump);
        }
        else if (player.GroundCondition() && !action.GetCatchInput())
        {
            if (action.GetXInput() != 0)
            {
                stateMachine.ChangeState(state.move);
            }
            else
            {
                stateMachine.ChangeState(state.idle);
            }
        }
        else if (InAirCondition())
        {
            stateMachine.ChangeState(state.inAir);
        }
    }

    private bool InAirCondition() => !sense.Wall() || (action.GetXInput() != movement.facingDireciton && !action.GetCatchInput());
}