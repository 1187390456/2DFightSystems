using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_Ability1 : E_Ability1
{
    public E_S_Ability1(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Ability1 abilityData) : base(stateMachine, entity, anmName, abilityData)
    {
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
            stateMachine.ChangeState(entity.meleeAttack);
        }
    }
}