using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1_Charge : E_Charge
{
    private Monster1 monster1;

    public Monster1_Charge(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Charge chargeData, Monster1 monster1) : base(stateMachine, entity, anmName, chargeData)
    {
        this.monster1 = monster1;
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
        if (entity.IsReachCanMeleeAttack())
        {
            stateMachine.ChangeState(monster1.meleeAttack);
        }
        else if (entity.IsProtect())
        {
            stateMachine.ChangeState(monster1.findPlayer);
        }
        else if (isChargeOver)
        {
            stateMachine.ChangeState(monster1.detected);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}