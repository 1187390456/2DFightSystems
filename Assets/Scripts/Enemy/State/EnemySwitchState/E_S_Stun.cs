using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_Stun : E_Stun
{
    public E_S_Stun(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Stun stunData) : base(stateMachine, entity, anmName, stunData)
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
                if (isStunOver)
                {
                    if (sense.MeleeAttack())
                    {
                        stateMachine.ChangeState(entity.meleeAttack);
                    }
                    else if (sense.MinDetected())
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
                if (isStunOver)
                {
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
                    else if (sense.MinDetected())
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