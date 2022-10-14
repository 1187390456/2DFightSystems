using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_CrouchIdle : P_Ground
{
    public P_CrouchIdle(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (!isExit)
        {
            if (player.GetYInput() != -1 && !player.CheckTop())
            {
                stateMachine.ChangeState(player.idle);
            }
            else if (player.GetXInput() != 0)
            {
                stateMachine.ChangeState(player.crouchMove);
            }
        }
    }
}