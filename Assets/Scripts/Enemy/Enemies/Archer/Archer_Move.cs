using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Move : E_Move
{
    public Archer archer;

    public Archer_Move(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Move moveData, Archer archer) : base(stateMachine, entity, anmName, moveData)
    {
        this.archer = archer;
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
            stateMachine.ChangeState(archer.meleeAttack);
        }
        else if (entity.CheckMinDetected())
        {
            // 远程攻击
            stateMachine.ChangeState(archer.move);
        }
        else if (entity.IsProtect())
        {
            stateMachine.ChangeState(archer.idle);
        }
    }
}