using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonster_RemoteAttack : E_RemoteAttack
{
    protected RemoteMonster remoteMonster;

    public RemoteMonster_RemoteAttack(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_RemoteAttack remoteAttackData, RemoteMonster remoteMonster) : base(stateMachine, entity, anmName, attackPos, remoteAttackData)
    {
        this.remoteMonster = remoteMonster;
    }

    public override void Update()
    {
        base.Update();
        if (isFinshAttack)
        {
            if (sense.MeleeAttack())
            {
                stateMachine.ChangeState(remoteMonster.meleeAttack);
            }
            else if (sense.MaxDetected()) return;
            else
            {
                stateMachine.ChangeState(remoteMonster.findPlayer);
            }
        }
    }
}