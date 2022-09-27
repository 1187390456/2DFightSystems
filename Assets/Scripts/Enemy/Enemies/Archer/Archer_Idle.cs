﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Idle : E_Idle
{
    public Archer archer;

    public Archer_Idle(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Idle idleData, Archer archer) : base(stateMachine, entity, anmName, idleData)
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
        if (entity.CheckMaxDetected())
        {
            SetTurn(false);
            stateMachine.ChangeState(archer.detected);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(archer.move);
        }
    }
}