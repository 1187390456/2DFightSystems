using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster_Charge : E_Charge
{
    protected MeleeMonster meleeMonster;

    public MeleeMonster_Charge(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Charge chargeData, MeleeMonster meleeMonster) : base(stateMachine, entity, anmName, chargeData)
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
        else if (sense.Protect())
        {
            stateMachine.ChangeState(meleeMonster.findPlayer);
        }
        else if (isChargeOver)
        {
            stateMachine.ChangeState(meleeMonster.detected);
        }
    }
}