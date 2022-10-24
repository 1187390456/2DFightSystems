using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freak_Stun : E_Stun
{
    protected Freak freak;

    public Freak_Stun(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Stun stunData, Freak freak) : base(stateMachine, entity, anmName, stunData)
    {
        this.freak = freak;
    }

    public override void Update()
    {
        base.Update();
        if (isStunOver)
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