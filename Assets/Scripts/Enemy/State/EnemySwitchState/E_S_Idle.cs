using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_Idle : E_Idle
{
    public E_S_Idle(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Idle idleData) : base(stateMachine, entity, anmName, idleData)
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
        if (sense.MinDetected())
        {
            SetTurn(false);
            stateMachine.ChangeState(entity.detected);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(entity.move);
        }
    }
}