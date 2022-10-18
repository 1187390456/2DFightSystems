using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Climb : P_Wall
{
    public P_Climb(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Update()
    {
        base.Update();
        if (!isExit)
        {
            if (action.GetYInput() > 0)
            {
                movement.SetVelocityY(data.climbSpeed);
            }
            else if (action.GetYInput() == 0)
            {
                stateMachine.ChangeState(state.catchWall);
            }
            else
            {
                stateMachine.ChangeState(state.slide);
            }
        }
    }
}