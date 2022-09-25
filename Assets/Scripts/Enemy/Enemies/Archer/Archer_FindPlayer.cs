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
        if (findPlayerTimeOver)
        {
            if (isFindPlayer)
            {
                if (entity.IsReachCanMeleeAttack())
                {
                    stateMachine.ChangeState(archer.meleeAttack);
                }
                else
                {
                    // 远程攻击
                    stateMachine.ChangeState(archer.move);
                }
            }
            else
            {
                stateMachine.ChangeState(archer.move);
            }
        }
    }
}