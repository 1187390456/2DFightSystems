using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonster_MeleeAttack : E_MeleeAttack
{
    protected RemoteMonster remoteMonster;

    public RemoteMonster_MeleeAttack(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_MeleeAttack meleeAttackData, RemoteMonster remoteMonster) : base(stateMachine, entity, anmName, attackPos, meleeAttackData)
    {
        this.remoteMonster = remoteMonster;
    }

    public override void Update()
    {
        base.Update();
        if (isFinshAttack)
        {
            if (sense.MeleeAttack()) return;
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