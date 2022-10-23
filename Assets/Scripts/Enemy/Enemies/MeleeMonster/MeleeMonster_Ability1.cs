using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster_Ability1 : E_Ability1
{
    protected MeleeMonster meleeMonster;

    public MeleeMonster_Ability1(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Ability1 ability1Data, MeleeMonster meleeMonster) : base(stateMachine, entity, anmName, ability1Data)
    {
        this.meleeMonster = meleeMonster;
    }

    public override void Update()
    {
        base.Update();
        if (isAbility1Over)
        {
            stateMachine.ChangeState(meleeMonster.meleeAttack);
        }
    }
}