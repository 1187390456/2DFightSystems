using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_E_Idle : P_Ground
{
    public P_E_Idle(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
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
        if (inputX != 0)
        {
            stateMachine.ChangeState(player.move);
        }
    }
}