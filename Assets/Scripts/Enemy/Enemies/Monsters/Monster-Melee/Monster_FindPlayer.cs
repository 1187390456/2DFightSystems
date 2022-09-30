using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_FindPlayer : E_FindPlayer
{
    private Monster Monster;

    public Monster_FindPlayer(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_FindPlayer findPlayerData, Monster Monster) : base(stateMachine, entity, anmName, findPlayerData)
    {
        this.Monster = Monster;
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
                    stateMachine.ChangeState(Monster.ability1);
                }
                else
                {
                    stateMachine.ChangeState(Monster.charge);
                }
            }
            else
            {
                stateMachine.ChangeState(Monster.move);
            }
        }
        else if (!entity.CheckMinDetected() && entity.CheckMaxDetected())
        {
            stateMachine.ChangeState(Monster.ability1);
        }
    }
}