using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonster_Idle : E_Idle
{
    protected RemoteMonster remoteMonster;

    public RemoteMonster_Idle(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Idle idleData, RemoteMonster remoteMonster) : base(stateMachine, entity, anmName, idleData)
    {
        this.remoteMonster = remoteMonster;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MinDetected())
        {
            SetTurn(false);
            stateMachine.ChangeState(remoteMonster.detected);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(remoteMonster.move);
        }
    }
}