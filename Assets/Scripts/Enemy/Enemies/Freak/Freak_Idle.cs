using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freak_Idle : E_Idle
{
    protected Freak freak;

    public Freak_Idle(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Idle idleData, Freak freak) : base(stateMachine, entity, anmName, idleData)
    {
        this.freak = freak;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MinDetected())
        {
            SetTurn(false);
            stateMachine.ChangeState(freak.detected);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(freak.move);
        }
    }
}