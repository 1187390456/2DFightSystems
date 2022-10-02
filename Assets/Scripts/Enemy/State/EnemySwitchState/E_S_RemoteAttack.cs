using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_RemoteAttack : E_RemoteAttack
{
    public E_S_RemoteAttack(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_RemoteAttack remoteAttackData) : base(stateMachine, entity, anmName, attackPos, remoteAttackData)
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
            if (entity.IsReachCanMeleeAttack())
            {
                if (entity.CheckCanDodge() && entity.entityData.canDodge)
                {
                    stateMachine.ChangeState(entity.dodge);
                }
                else
                {
                    stateMachine.ChangeState(entity.meleeAttack);
                }
            }
            else if (entity.CheckMinDetected())
            {
                return;
            }
            else if (entity.CheckMaxDetected() && entity.entityData.canAbility1)
            {
                stateMachine.ChangeState(entity.ability1);
            }
            else
            {
                stateMachine.ChangeState(entity.findPlayer);
            }
        }
    }
}