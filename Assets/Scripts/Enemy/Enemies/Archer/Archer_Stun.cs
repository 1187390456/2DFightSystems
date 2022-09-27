﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Stun : E_Stun
{
    public Archer archer;

    public Archer_Stun(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Stun stunData, Archer archer) : base(stateMachine, entity, anmName, stunData)
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
        if (isStunOver)
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
            else if (entity.CheckMaxDetected())
            {
                stateMachine.ChangeState(archer.remoteAttack);
            }
            else
            {
                stateMachine.ChangeState(archer.findPlayer);
            }
        }
    }
}