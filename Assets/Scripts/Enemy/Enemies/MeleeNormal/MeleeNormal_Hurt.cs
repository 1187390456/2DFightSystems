using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeNormal_Hurt : E_Hurt
{
    protected MeleeNormal meleeNormal;

    public MeleeNormal_Hurt(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Hurt hurtData, MeleeNormal meleeNormal) : base(stateMachine, entity, anmName, hurtData)
    {
        this.meleeNormal = meleeNormal;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MeleeAttack())
        {
            stateMachine.ChangeState(meleeNormal.meleeAttack);
        }
        else if (isHurtOver)
        {
            if (sense.MinDetected())
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