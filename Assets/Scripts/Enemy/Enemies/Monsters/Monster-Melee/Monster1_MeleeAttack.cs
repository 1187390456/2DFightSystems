using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1_MeleeAttack : E_MeleeAttack
{
    private Monster1 monster1;

    public Monster1_MeleeAttack(E_StateMachine stateMachine, E_Entity entity, string anmName, Transform attackPos, D_E_MeleeAttack meleeAttackData, Monster1 monster1) : base(stateMachine, entity, anmName, attackPos, meleeAttackData)
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
                stateMachine.ChangeState(monster1.charge);
            }
            else
            {
                stateMachine.ChangeState(monster1.findPlayer);
            }
        }
    }
}