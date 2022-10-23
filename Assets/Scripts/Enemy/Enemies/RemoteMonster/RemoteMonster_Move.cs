using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonster_Move : E_Move
{
    protected RemoteMonster remoteMonster;

    public RemoteMonster_Move(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Move moveData, RemoteMonster remoteMonster) : base(stateMachine, entity, anmName, moveData)
    {
        this.remoteMonster = remoteMonster;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MaxDetected())
        {
            stateMachine.ChangeState(remoteMonster.detected);
        }
        else if (sense.Protect())
        {
            stateMachine.ChangeState(remoteMonster.idle);
        }
    }
}