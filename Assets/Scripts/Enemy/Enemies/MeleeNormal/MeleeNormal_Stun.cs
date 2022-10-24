using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeNormal_Stun : E_Stun
{
    protected MeleeNormal meleeNormal;

    public MeleeNormal_Stun(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Stun stunData, MeleeNormal meleeNormal) : base(stateMachine, entity, anmName, stunData)
    {
        this.meleeNormal = meleeNormal;
    }

    public override void Update()
    {
        base.Update();
        if (isStunOver)
        {
            if (sense.MeleeAttack())
            {
                stateMachine.ChangeState(meleeNormal.meleeAttack);
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