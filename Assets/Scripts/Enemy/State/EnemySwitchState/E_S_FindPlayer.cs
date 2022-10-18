using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_FindPlayer : E_FindPlayer
{
    public E_S_FindPlayer(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_FindPlayer findPlayerData) : base(stateMachine, entity, anmName, findPlayerData)
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
                if (findPlayerTimeOver)
                {
                    if (isFindPlayer)
                    {
                        if (entity.IsProtect())
                        {
                            stateMachine.ChangeState(entity.idle);
                        }
                        else
                        {
                            stateMachine.ChangeState(entity.charge);
                        }
                    }
                    else
                    {
                        stateMachine.ChangeState(entity.move);
                    }
                }
                else if (!sense.MinDetected() && sense.MaxDetected() && entity.entityData.canAbility1)
                {
                    stateMachine.ChangeState(entity.ability1);
                }
                break;

            case Enemy.EnemyType.Remote:
                if (findPlayerTimeOver)
                {
                    if (isFindPlayer)
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
                        else
                        {
                            stateMachine.ChangeState(entity.remoteAttack);
                        }
                    }
                    else
                    {
                        stateMachine.ChangeState(entity.move);
                    }
                }
                else if (!sense.MinDetected() && sense.MaxDetected() && entity.entityData.canAbility1)
                {
                    stateMachine.ChangeState(entity.ability1);
                }
                break;

            default:
                break;
        }
    }
}