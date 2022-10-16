using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_KnockBack : P_Ability
{
    protected float startKnockbackTime;

    public P_KnockBack(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        startKnockbackTime = Time.time;
        movement.SetVelocity(playerData.knockbackSpeed, playerData.knockbackAngle, player.knockBackDirection);
    }

    public override void Update()
    {
        base.Update();
        if (Time.time >= startKnockbackTime + playerData.knockbackTime)
        {
            movement.SetVelocityX(0.0f);
            isAbilityDone = true;
        }
    }
}