using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_MeleeAttack : E_MeleeAttack
{
    public E_S_MeleeAttack(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_MeleeAttack meleeAttackData) : base(stateMachine, entity, anmName, attackPos, meleeAttackData)
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
        switch (entity.entityData.enemyType)
        {
            case Enemy.EnemyType.Melee:
                if (isFinshAttack)
                {
                    if (entity.IsReachCanMeleeAttack())
                    {
                        return;
                    }
                    else if (entity.CheckMinDetected())
                    {
                        stateMachine.ChangeState(entity.charge);
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
                break;

            case Enemy.EnemyType.Remote:
                if (isFinshAttack)
                {
                    if (entity.CheckCanDodge() && entity.entityData.canDodge)
                    {
                        stateMachine.ChangeState(entity.dodge);
                    }
                    else if (entity.IsReachCanMeleeAttack())
                    {
                        return;
                    }
                    else if (entity.CheckMinDetected())
                    {
                        stateMachine.ChangeState(entity.remoteAttack);
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
                break;

            default:
                break;
        }
    }
}