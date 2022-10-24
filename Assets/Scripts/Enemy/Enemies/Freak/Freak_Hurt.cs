using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freak_Hurt : E_Hurt
{
    protected Freak freak;

    public Freak_Hurt(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Hurt hurtData, Freak freak) : base(stateMachine, entity, anmName, hurtData)
    {
        this.freak = freak;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MeleeAttack())
        {
            stateMachine.ChangeState(freak.remoteAttack);
        }
        else if (isHurtOver)
        {
            if (sense.MaxDetected())
            {
                stateMachine.ChangeState(freak.remoteAttack);
            }
            else
            {
                stateMachine.ChangeState(freak.findPlayer);
            }
        }
    }
}