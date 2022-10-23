using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonster_Stun : E_Stun
{
    protected RemoteMonster remoteMonster;

    public RemoteMonster_Stun(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Stun stunData, RemoteMonster remoteMonster) : base(stateMachine, entity, anmName, stunData)
    {
        this.remoteMonster = remoteMonster;
    }

    public override void Update()
    {
        base.Update();
        if (isStunOver)
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