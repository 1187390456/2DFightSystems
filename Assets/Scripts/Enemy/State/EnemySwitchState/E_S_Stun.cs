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
                    if (entity.IsReachCanMeleeAttack())
                    {
                        stateMachine.ChangeState(entity.meleeAttack);
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
                if (isStunOver)
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