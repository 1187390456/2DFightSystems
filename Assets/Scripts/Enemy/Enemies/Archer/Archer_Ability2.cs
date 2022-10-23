using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Ability2 : E_Ability2
{
    protected Archer archer;

    public Archer_Ability2(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_RemoteAttack remoteAttackData, D_E_Ability2 ability2Data, Archer archer) : base(stateMachine, entity, anmName, attackPos, remoteAttackData, ability2Data)
    {
        this.archer = archer;
    }

    public override void Update()
    {
        base.Update();
        if (isAbility2Over)
        {
            stateMachine.ChangeState(archer.move);
        }
    }
}