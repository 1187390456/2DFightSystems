using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freak_Ability2 : E_Ability2
{
    protected Freak freak;

    public Freak_Ability2(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_RemoteAttack remoteAttackData, D_E_Ability2 ability2Data, Freak freak) : base(stateMachine, entity, anmName, attackPos, remoteAttackData, ability2Data)
    {
        this.freak = freak;
    }

    public override void Update()
    {
        base.Update();
        if (isAbility2Over)
        {
            stateMachine.ChangeState(freak.move);
        }
    }
}