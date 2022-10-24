using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeNormal_Idle : E_Idle
{
    protected MeleeNormal meleeNormal;

    public MeleeNormal_Idle(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Idle idleData, MeleeNormal meleeNormal) : base(stateMachine, entity, anmName, idleData)
    {
        this.meleeNormal = meleeNormal;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MinDetected())
        {
            SetTurn(false);
            stateMachine.ChangeState(meleeNormal.detected);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(meleeNormal.move);
        }
    }
}