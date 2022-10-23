using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonster_Detected : E_Detected
{
    protected RemoteMonster remoteMonster;

    public RemoteMonster_Detected(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Detected detectedData, RemoteMonster remoteMonster) : base(stateMachine, entity, anmName, detectedData)
    {
        this.remoteMonster = remoteMonster;
    }

    public override void Update()
    {
        base.Update();
        if (isDetectedOver)
        {
            if (sense.MeleeAttack())
            {
                stateMachine.ChangeState(remoteMonster.meleeAttack);
            }
            else if (sense.MaxDetected())
            {
                stateMachine.ChangeState(remoteMonster.remoteAttack);
            }
            else
            {
                stateMachine.ChangeState(remoteMonster.findPlayer);
            }
        }
    }
}