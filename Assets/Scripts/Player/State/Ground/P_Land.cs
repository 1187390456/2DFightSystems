using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Land : P_Ground
{
    public P_Land(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
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
            movement.SetVelocityZero();
            if (isAnimationDone)
            {
                stateMachine.ChangeState(state.idle);
            }
            else if (action.GetXInput() != 0)
            {
                stateMachine.ChangeState(state.move);
            }
        }
    }
}