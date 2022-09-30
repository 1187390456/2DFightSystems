using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Ability1 : E_Ability1
{
    private Monster Monster;

    public Monster_Ability1(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Ability1 abilityData, Monster Monster) : base(stateMachine, entity, anmName, abilityData)
    {
        this.Monster = Monster;
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
            stateMachine.ChangeState(Monster.meleeAttack);
        }
    }
}