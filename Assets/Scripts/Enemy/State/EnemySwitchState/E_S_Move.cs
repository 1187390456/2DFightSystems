using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_Move : E_Move
{
    public E_S_Move(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Move moveData) : base(stateMachine, entity, anmName, moveData)
    {
    }

    public override void Update()
    {
        base.Update();
        switch (entity.entityData.enemyType)
        {
            case Enemy.EnemyType.Melee:
                if (sense.MinDetected())
                {
                    stateMachine.ChangeState(state.detected);
                }
                else if (entity.IsProtect())
                {
                    stateMachine.ChangeState(state.idle);
                }
                break;

            case Enemy.EnemyType.Remote:
                if (sense.MaxDetected())
                {
                    stateMachine.ChangeState(state.detected);
                }
                else if (entity.IsProtect())
                {
                    stateMachine.ChangeState(state.idle);
                }
                break;

            default:
                break;
        }
    }
}