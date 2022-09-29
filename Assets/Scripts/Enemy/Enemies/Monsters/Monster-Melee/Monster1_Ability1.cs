using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1_Ability1 : E_Ability1
{
    private Monster1 monster1;

    public Monster1_Ability1(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Ability1 abilityData, Monster1 monster1) : base(stateMachine, entity, anmName, abilityData)
    {
        this.monster1 = monster1;
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
            stateMachine.ChangeState(monster1.meleeAttack);
        }
    }
}