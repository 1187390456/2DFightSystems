using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_Move : E_Move
{
    public E_S_Move(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Move moveData) : base(stateMachine, entity, anmName, moveData)
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
                if (entity.CheckMinDetected())
                {
                    stateMachine.ChangeState(entity.detected);
                }
                else if (entity.IsProtect())
                {
                    stateMachine.ChangeState(entity.idle);
                }
                break;

            case Enemy.EnemyType.Remote:
                if (entity.CheckMaxDetected())
                {
                    stateMachine.ChangeState(entity.detected);
                }
                else if (entity.IsProtect())
                {
                    stateMachine.ChangeState(entity.idle);
                }
                break;

            default:
                break;
        }
    }
}