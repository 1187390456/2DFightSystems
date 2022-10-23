using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonster_Ability2 : E_Ability2
{
    protected RemoteMonster remoteMonster;

    public RemoteMonster_Ability2(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_RemoteAttack remoteAttackData, D_E_Ability2 ability2Data, RemoteMonster remoteMonster) : base(stateMachine, entity, anmName, attackPos, remoteAttackData, ability2Data)
    {
        this.remoteMonster = remoteMonster;
    }

    public override void Update()
    {
        base.Update();
        if (isAbility2Over)
        {
            stateMachine.ChangeState(remoteMonster.move);
        }
    }
}