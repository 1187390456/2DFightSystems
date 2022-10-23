using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster_Move : E_Move
{
    protected MeleeMonster meleeMonster;

    public MeleeMonster_Move(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Move moveData, MeleeMonster meleeMonster) : base(stateMachine, entity, anmName, moveData)
    {
        this.meleeMonster = meleeMonster;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MinDetected())
        {
            stateMachine.ChangeState(meleeMonster.detected);
        }
        else if (sense.Protect())
        {
            stateMachine.ChangeState(meleeMonster.idle);
        }
    }
}