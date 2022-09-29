using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1_FindPlayer : E_FindPlayer
{
    private Monster1 monster1;

    public Monster1_FindPlayer(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_FindPlayer findPlayerData, Monster1 monster1) : base(stateMachine, entity, anmName, findPlayerData)
    {
        this.monster1 = monster1;
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
        if (findPlayerTimeOver)
        {
            if (isFindPlayer)
            {
                if (entity.IsProtect())
                {
                    stateMachine.ChangeState(monster1.move);
                }
                else
                {
                    stateMachine.ChangeState(monster1.charge);
                }
            }
            else
            {
                stateMachine.ChangeState(monster1.move);
            }
        }
        else if (!entity.CheckMinDetected() && entity.CheckMaxDetected())
        {
            stateMachine.ChangeState(monster1.ability1);
        }
    }
}