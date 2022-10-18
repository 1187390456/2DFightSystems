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
                if (sense.MeleeAttack())
                {
                    stateMachine.ChangeState(entity.meleeAttack);
                }
                else if (isHurtOver)
                {
                    if (sense.MinDetected())
                    {
                        stateMachine.ChangeState(entity.charge);
                    }
                    else if (sense.MaxDetected() && entity.entityData.canAbility1)
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
                if (sense.MeleeAttack())
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
                    if (sense.MinDetected())
                    {
                        stateMachine.ChangeState(entity.remoteAttack);
                    }
                    else if (sense.MaxDetected() && entity.entityData.canAbility1)
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