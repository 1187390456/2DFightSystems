using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Ability1 : E_Ability1
{
    private Archer archer;

    public Archer_Ability1(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Ability1 abilityData, Archer archer) : base(stateMachine, entity, anmName, abilityData)
    {
        this.archer = archer;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
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