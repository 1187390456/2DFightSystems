using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Silde : P_Wall
{
    public P_Silde(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Update()
    {
        base.Update();
        if (!isExit)
        {
            if (action.GetCatchInput() && action.GetYInput() >= 0)
            {
                stateMachine.ChangeState(player.catchWall);
            }
            else
            {
                movement.SetVelocitY(-playerData.sildeSpeed);
            }
        }
    }
}