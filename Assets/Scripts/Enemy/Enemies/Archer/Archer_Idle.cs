using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Idle : E_Idle
{
    protected Archer archer;

    public Archer_Idle(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Idle idleData, Archer archer) : base(stateMachine, entity, anmName, idleData)
    {
        this.archer = archer;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MinDetected())
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