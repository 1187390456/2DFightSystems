using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_CrouchMove : P_Ground
{
    public P_CrouchMove(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
            player.SetPlayerMove(playerData.crouchMoveSpeed);
            player.CheckTurn();
            if (player.GetYInput() != -1 && !player.CheckTop())
            {
                stateMachine.ChangeState(player.move);
            }
            else if (player.GetXInput() == 0)
            {
                stateMachine.ChangeState(player.crouchIdle);
            }
        }
    }
}