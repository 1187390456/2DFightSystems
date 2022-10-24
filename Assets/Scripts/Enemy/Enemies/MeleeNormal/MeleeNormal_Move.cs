using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeNormal_Move : E_Move
{
    protected MeleeNormal meleeNormal;

    public MeleeNormal_Move(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Move moveData, MeleeNormal meleeNormal) : base(stateMachine, entity, anmName, moveData)
    {
        this.meleeNormal = meleeNormal;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MinDetected())
        {
            stateMachine.ChangeState(meleeNormal.detected);
        }
        else if (sense.Protect())
        {
            stateMachine.ChangeState(meleeNormal.idle);
        }
    }
}