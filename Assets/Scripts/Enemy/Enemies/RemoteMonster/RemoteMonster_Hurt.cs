using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonster_Hurt : E_Hurt
{
    protected RemoteMonster remoteMonster;

    public RemoteMonster_Hurt(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Hurt hurtData, RemoteMonster remoteMonster) : base(stateMachine, entity, anmName, hurtData)
    {
        this.remoteMonster = remoteMonster;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MeleeAttack())
        {
            stateMachine.ChangeState(remoteMonster.meleeAttack);
        }
        else if (isHurtOver)
        {
            if (sense.MaxDetected())
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