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
        player.jump.ResetJumpCount();
    }

    public override void Exit()
    {
        base.Exit();
        player.inAir.StartGraceTime();
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
            stateMachine.ChangeState(player.firstAttack);
        }
        else if (player.SecondAttackCondition())
        {
            stateMachine.ChangeState(player.secondAttack);
        }
        else if (player.DashCondition())
        {
            stateMachine.ChangeState(player.dash);
        }
        else if (player.JumpCondition())
        {
            stateMachine.ChangeState(player.jump);
        }
        else if (player.CatchWallConditon())
        {
            stateMachine.ChangeState(player.catchWall);
        }
        else if (!player.GroundCondition())
        {
            stateMachine.ChangeState(player.inAir);
        }
    }
}