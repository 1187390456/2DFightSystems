using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_Charge : E_Charge
{
    public E_S_Charge(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Charge chargeData) : base(stateMachine, entity, anmName, chargeData)
    {
    }

    public override void Update()
    {
        base.Update();
        if (sense.MeleeAttack())
        {
            stateMachine.ChangeState(state.meleeAttack);
        }
        else if (entity.IsProtect())
        {
            stateMachine.ChangeState(state.findPlayer);
        }
        else if (isChargeOver)
        {
            stateMachine.ChangeState(state.detected);
        }
    }
}