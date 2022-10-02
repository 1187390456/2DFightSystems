using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_Hurt : E_Hurt
{
    public E_S_Hurt(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Hurt hurtData) : base(stateMachine, entity, anmName, hurtData)
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
        switch (entity.entityData.enemyType)
        {
            case Enemy.EnemyType.Melee:
                if (entity.IsReachCanMeleeAttack())
                {
                    stateMachine.ChangeState(entity.meleeAttack);
                }
                else if (isHurtOver)
                {
                    if (entity.CheckMinDetected())
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
                else if (isHurtOver)
                {
                    if (entity.CheckMinDetected())
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