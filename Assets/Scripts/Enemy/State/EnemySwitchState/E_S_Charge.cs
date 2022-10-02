using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_Charge : E_Charge
{
    public E_S_Charge(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Charge chargeData) : base(stateMachine, entity, anmName, chargeData)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (entity.IsReachCanMeleeAttack())
        {
            stateMachine.ChangeState(entity.meleeAttack);
        }
        else if (entity.IsProtect())
        {
            stateMachine.ChangeState(entity.findPlayer);
        }
        else if (isChargeOver)
        {
            stateMachine.ChangeState(entity.detected);
        }
    }
}