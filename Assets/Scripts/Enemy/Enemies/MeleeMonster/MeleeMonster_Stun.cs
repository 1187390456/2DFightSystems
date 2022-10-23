using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster_Stun : E_Stun
{
    protected MeleeMonster meleeMonster;

    public MeleeMonster_Stun(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Stun stunData, MeleeMonster meleeMonster) : base(stateMachine, entity, anmName, stunData)
    {
        this.meleeMonster = meleeMonster;
    }

    public override void Update()
    {
        base.Update();
        if (isStunOver)
        {
            if (sense.MeleeAttack())
            {
                stateMachine.ChangeState(meleeMonster.meleeAttack);
            }
            else if (sense.MinDetected())
            {
                stateMachine.ChangeState(meleeMonster.charge);
            }
            else if (sense.MaxDetected())
            {
                stateMachine.ChangeState(meleeMonster.ability1);
            }
            else
            {
                stateMachine.ChangeState(meleeMonster.findPlayer);
            }
        }
    }
}