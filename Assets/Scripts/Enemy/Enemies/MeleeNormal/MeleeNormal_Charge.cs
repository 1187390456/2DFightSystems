using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeNormal_Charge : E_Charge
{
    protected MeleeNormal meleeNormal;

    public MeleeNormal_Charge(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Charge chargeData, MeleeNormal meleeNormal) : base(stateMachine, entity, anmName, chargeData)
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
        else if (sense.Protect())
        {
            stateMachine.ChangeState(meleeNormal.findPlayer);
        }
        else if (isChargeOver)
        {
            stateMachine.ChangeState(meleeNormal.detected);
        }
    }
}