using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster_Dead : E_Dead
{
    protected MeleeMonster meleeMonster;

    public MeleeMonster_Dead(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Dead deadData, MeleeMonster meleeMonster) : base(stateMachine, entity, anmName, deadData)
    {
        this.meleeMonster = meleeMonster;
    }

    public override void Update()
    {
        base.Update();
        if (isdeadOver)
        {
            stateMachine.ChangeState(meleeMonster.move);
        }
    }
}