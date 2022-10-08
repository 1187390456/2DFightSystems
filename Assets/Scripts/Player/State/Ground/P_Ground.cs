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
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (player.JumpCondition())
        {
            player.UseJumpInput();
            stateMachine.ChangeState(player.jump);
        }
        else if (!player.GroundCondition())
        {
            player.inAir.StartGraceTime();
            stateMachine.ChangeState(player.inAir);
        }
    }
}