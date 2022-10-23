using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster_MeleeAttack : E_MeleeAttack
{
    protected MeleeMonster meleeMonster;

    public MeleeMonster_MeleeAttack(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_MeleeAttack meleeAttackData, MeleeMonster meleeMonster) : base(stateMachine, entity, anmName, attackPos, meleeAttackData)
    {
        this.meleeMonster = meleeMonster;
    }

    public override void Update()
    {
        base.Update();
        if (isFinshAttack)
        {
            if (sense.MeleeAttack())
            {
                return;
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