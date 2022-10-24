using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freak_RemoteAttack : E_RemoteAttack
{
    protected Freak freak;

    public Freak_RemoteAttack(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_RemoteAttack remoteAttackData, Freak freak) : base(stateMachine, entity, anmName, attackPos, remoteAttackData)
    {
        this.freak = freak;
    }

    public override void Update()
    {
        base.Update();
        if (isFinshAttack)
        {
            if (sense.MaxDetected()) return;
            else
            {
                stateMachine.ChangeState(freak.findPlayer);
            }
        }
    }
}