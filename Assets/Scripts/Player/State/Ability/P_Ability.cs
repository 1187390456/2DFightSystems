using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Ability : P_State
{
    protected bool isAbilityDone;

    public P_Ability(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
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
        if (isAbilityDone)
        {
            if (player.GroundCondition())
            {
                stateMachine.ChangeState(player.idle);
            }
            else
            {
                stateMachine.ChangeState(player.inAir);
            }
        }
    }
}