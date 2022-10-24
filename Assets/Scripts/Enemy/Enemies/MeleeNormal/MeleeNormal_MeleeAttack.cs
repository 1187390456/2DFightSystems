using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeNormal_MeleeAttack : E_MeleeAttack
{
    protected MeleeNormal meleeNormal;

    public MeleeNormal_MeleeAttack(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_MeleeAttack meleeAttackData, MeleeNormal meleeNormal) : base(stateMachine, entity, anmName, attackPos, meleeAttackData)
    {
        this.meleeNormal = meleeNormal;
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
                stateMachine.ChangeState(meleeNormal.charge);
            }
            else
            {
                stateMachine.ChangeState(meleeNormal.findPlayer);
            }
        }
    }
}