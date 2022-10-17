using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Ground : P_State
{
    public P_Ground(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
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
        state.inAir.StartGraceTime();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (player.FirstAttackCondition())
        {
            stateMachine.ChangeState(state.firstAttack);
        }
        else if (player.SecondAttackCondition())
        {
            stateMachine.ChangeState(state.secondAttack);
        }
        else if (player.DashCondition())
        {
            stateMachine.ChangeState(state.dash);
        }
        else if (player.JumpCondition())
        {
            stateMachine.ChangeState(state.jump);
        }
        else if (player.CatchWallConditon())
        {
            stateMachine.ChangeState(state.catchWall);
        }
        else if (!player.GroundCondition())
        {
            stateMachine.ChangeState(state.inAir);
        }
    }
}