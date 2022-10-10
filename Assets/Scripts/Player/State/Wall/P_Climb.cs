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
        if (player.GetYInput() > 0)
        {
            player.SetVelocitY(playerData.climbSpeed);
        }
        else if (player.GetYInput() == 0)
        {
            stateMachine.ChangeState(player.catchWall);
        }
        else
        {
            stateMachine.ChangeState(player.slide);
        }
    }
}