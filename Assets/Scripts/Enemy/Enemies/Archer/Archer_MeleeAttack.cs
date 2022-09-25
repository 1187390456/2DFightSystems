using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_MeleeAttack : E_MeleeAttack
{
    public Archer archer;

    public Archer_MeleeAttack(E_StateMachine stateMachine, E_Entity entity, string anmName, Transform attackPos, D_E_MeleeAttack meleeAttackData, Archer archer) : base(stateMachine, entity, anmName, attackPos, meleeAttackData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void StartAttack()
    {
        base.StartAttack();
    }

    public override void Update()
    {
        base.Update();
        if (isFinshAttack)
        {
            if (entity.CheckMinDetected())
            {
                // 远程攻击
                stateMachine.ChangeState(archer.move);
            }
            else
            {
                stateMachine.ChangeState(archer.findPlayer);
            }
        }
    }
}