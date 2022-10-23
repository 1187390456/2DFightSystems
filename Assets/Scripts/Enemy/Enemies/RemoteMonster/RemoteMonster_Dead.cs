using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonster_Dead : E_Dead
{
    protected RemoteMonster remoteMonster;

    public RemoteMonster_Dead(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Dead deadData, RemoteMonster remoteMonster) : base(stateMachine, entity, anmName, deadData)
    {
        this.remoteMonster = remoteMonster;
    }

    public override void Update()
    {
        base.Update();
        if (isdeadOver)
        {
            stateMachine.ChangeState(remoteMonster.move);
        }
    }
}