using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster_Hurt : E_Hurt
{
    protected MeleeMonster meleeMonster;

    public MeleeMonster_Hurt(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Hurt hurtData, MeleeMonster meleeMonster) : base(stateMachine, entity, anmName, hurtData)
    {
        this.meleeMonster = meleeMonster;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MeleeAttack())
        {
            stateMachine.ChangeState(meleeMonster.meleeAttack);
        }
        else if (isHurtOver)
        {
            if (sense.MinDetected())
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