using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freak_Move : E_Move
{
    protected Freak freak;

    public Freak_Move(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Move moveData, Freak freak) : base(stateMachine, entity, anmName, moveData)
    {
        this.freak = freak;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MaxDetected())
        {
            stateMachine.ChangeState(freak.detected);
        }
        else if (sense.Protect())
        {
            stateMachine.ChangeState(freak.idle);
        }
    }
}