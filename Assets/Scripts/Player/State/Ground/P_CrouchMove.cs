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

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (!isExit)
        {
            movement.SetPlayerMove(data.crouchMoveSpeed);
            movement.CheckTurn();
            if (action.GetYInput() != -1 && !sense.Top())
            {
                stateMachine.ChangeState(state.move);
            }
            else if (action.GetXInput() == 0)
            {
                stateMachine.ChangeState(state.crouchIdle);
            }
        }
    }
}