using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeNormal_Detected : E_Detected
{
    protected MeleeNormal meleeNormal;

    public MeleeNormal_Detected(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Detected detectedData, MeleeNormal meleeNormal) : base(stateMachine, entity, anmName, detectedData)
    {
        this.meleeNormal = meleeNormal;
    }

    public override void Update()
    {
        base.Update();
        if (isDetectedOver)
        {
            if (sense.MeleeAttack())
            {
                stateMachine.ChangeState(meleeNormal.meleeAttack);
            }
            else if (sense.MinDetected())
            {
                stateMachine.ChangeState(meleeNormal.charge);
            }
            else
            {
                stateMachine.ChangeState(meleeNormal.findPlayer);
            }
        }
    }
}