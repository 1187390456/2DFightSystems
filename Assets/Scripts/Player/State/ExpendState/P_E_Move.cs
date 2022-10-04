using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_E_Move : P_B_Ground
{
    public P_E_Move(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
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

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(playerData.moveSpeed * inputX);
        player.CheckTurn();
        if (inputX == 0.0f)
        {
            stateMachine.ChangeState(player.idle);
        }
    }
}