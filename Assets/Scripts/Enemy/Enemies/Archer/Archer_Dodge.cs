using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Dodge : E_Dodge
{
    protected Archer archer;

    public Archer_Dodge(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Dodge dodgeData, Archer archer) : base(stateMachine, entity, anmName, dodgeData)
    {
        this.archer = archer;
    }

    public override void Update()
    {
        base.Update();
        if (isDodgeOver)
        {
            stateMachine.ChangeState(archer.remoteAttack);
        }
    }
}