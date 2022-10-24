using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freak_Detected : E_Detected
{
    protected Freak freak;

    public Freak_Detected(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Detected detectedData, Freak freak) : base(stateMachine, entity, anmName, detectedData)
    {
        this.freak = freak;
    }

    public override void Update()
    {
        base.Update();
        if (isDetectedOver)
        {
            if (sense.MaxDetected())
            {
                stateMachine.ChangeState(freak.remoteAttack);
            }
            else
            {
                stateMachine.ChangeState(freak.findPlayer);
            }
        }
    }
}