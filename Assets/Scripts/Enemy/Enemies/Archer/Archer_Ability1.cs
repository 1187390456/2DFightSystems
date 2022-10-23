using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Ability1 : E_Ability1
{
    protected Archer archer;

    public Archer_Ability1(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Ability1 ability1Data, Archer archer) : base(stateMachine, entity, anmName, ability1Data)
    {
        this.archer = archer;
    }

    public override void Update()
    {
        base.Update();
        if (isAbility1Over)
        {
            stateMachine.ChangeState(archer.meleeAttack);
        }
    }
}