using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_KnockBack : P_State
{
    protected float startKnockbackTime;

    public P_KnockBack(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        startKnockbackTime = Time.time;
        player.SetVelocity(playerData.knockbackSpeed, playerData.knockbackAngle, player.knockBackDirection);
    }

    public override void Update()
    {
        base.Update();
        if (Time.time >= startKnockbackTime + playerData.knockbackTime)
        {
            player.SetVelocityX(0.0f);
            stateMachine.ChangeState(player.inAir);
        }
    }
}