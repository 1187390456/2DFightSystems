using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Charge : E_Charge
{
    private Monster Monster;

    public Monster_Charge(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Charge chargeData, Monster Monster) : base(stateMachine, entity, anmName, chargeData)
    {
        this.Monster = Monster;
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
            stateMachine.ChangeState(Monster.meleeAttack);
        }
        else if (entity.IsProtect())
        {
            stateMachine.ChangeState(Monster.findPlayer);
        }
        else if (isChargeOver)
        {
            stateMachine.ChangeState(Monster.detected);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}