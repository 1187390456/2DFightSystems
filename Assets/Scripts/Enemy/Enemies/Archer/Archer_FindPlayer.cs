using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_FindPlayer : E_FindPlayer
{
    public Archer archer;

    public Archer_FindPlayer(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_FindPlayer findPlayerData, Archer archer) : base(stateMachine, entity, anmName, findPlayerData)
    {
        this.archer = archer;
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
        // 这里由于按最小警备 所以外层要最大警备判断
        if (findPlayerTimeOver)
        {
            if (isFindPlayer)
            {
                if (entity.IsReachCanMeleeAttack())
                {
                    if (archer.CheckCanDodge())
                    {
                        stateMachine.ChangeState(archer.dodge);
                    }
                    else
                    {
                        stateMachine.ChangeState(archer.meleeAttack);
                    }
                }
                else
                {
                    stateMachine.ChangeState(archer.ability1);
                }
            }
            else
            {
                stateMachine.ChangeState(archer.move);
            }
        }
        else if (!entity.CheckMinDetected() && entity.CheckMaxDetected())
        {
            stateMachine.ChangeState(archer.remoteAttack);
        }
    }
}