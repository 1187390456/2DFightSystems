using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_Detected : E_Detected
{
    public E_S_Detected(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Detected detectedData) : base(stateMachine, entity, anmName, detectedData)
    {
    }

    public override void Update()
    {
        base.Update();
        switch (entity.entityData.enemyType)
        {
            case Enemy.EnemyType.Melee:
                if (isDetectedOver)
                {
                    if (sense.MeleeAttack())
                    {
                        stateMachine.ChangeState(state.meleeAttack);
                    }
                    else if (sense.MinDetected())
                    {
                        stateMachine.ChangeState(state.charge);
                    }
                    else if (sense.MaxDetected() && entity.entityData.canAbility1)
                    {
                        stateMachine.ChangeState(state.ability1);
                    }
                    else
                    {
                        stateMachine.ChangeState(state.findPlayer);
                    }
                }
                break;

            case Enemy.EnemyType.Remote:
                if (isDetectedOver)
                {
                    if (sense.MeleeAttack())
                    {
                        if (entity.CheckCanDodge() && entity.entityData.canDodge)
                        {
                            stateMachine.ChangeState(state.dodge);
                        }
                        else
                        {
                            stateMachine.ChangeState(state.meleeAttack);
                        }
                    }
                    else if (sense.MinDetected())
                    {
                        stateMachine.ChangeState(state.remoteAttack);
                    }
                    else if (sense.MaxDetected() && entity.entityData.canAbility1)
                    {
                        stateMachine.ChangeState(state.ability1);
                    }
                    else
                    {
                        stateMachine.ChangeState(state.findPlayer);
                    }
                }
                break;

            default:
                break;
        }
    }
}